using Microsoft.AspNetCore.Mvc;
using TicketVerkoop.Util.Mail.Interfaces;
using TicketVerkoop.Util.PDF.Interfaces;
using TicketVerkoop.ViewModels;
using TicketVerkoop.Domains;
using TicketVerkoop.Extentions;
using TicketVerkoop.Domains.Entities;
using AutoMapper;
using TicketVerkoop.Services.Interfaces;
using System.Diagnostics;

namespace TicketVerkoop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICreatePDF _createPDF;
        private readonly IEmailSend _emailSender;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper mapper;
        private readonly IService<Bestelling> bestellingService;

        public ShoppingCartController(IEmailSend emailSend,
            ICreatePDF createPDF, 
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper,
            IService<Bestelling> bestellingService)
        {
            _createPDF = createPDF;
            _emailSender = emailSend;
            _hostingEnvironment = webHostEnvironment;
            this.mapper = mapper;
            this.bestellingService = bestellingService;
        }

        // GET: ShoppingCartController
        public IActionResult Index()
        {
            ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            //call SessionID
            //var SessiondId = HttpContext.Session.Id;
            return View(cartList);
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            ShoppingCartVM? shoppingCartVM = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            var bestellingVM = new BestelllingVM
            {
                AbonnementId = 1,
                UserId = 1,
                BestelDatum = DateTime.Now
            };
            try
            {
                var bestelling = mapper.Map<Bestelling>(bestellingVM);
                var bestellingID = bestellingService.AddandGetID(bestelling);
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }

            List<TicketVM> ticketList = shoppingCartVM.Tickets;
            return View("Thanks");
        }

        //ALLES VAN MAIL
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(MailVM mailVM)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    string pdfFile = "Ticket" + DateTime.Now.Year;
                    var pdfFileName = $"{pdfFile}_{Guid.NewGuid()}.pdf";
                    var bestellings = new List<Domains.Entities.Bestelling>
            {
                new Domains.Entities.Bestelling { BestellingId = 1, BestelDatum = DateTime.Now },
            };
                    //het pad naar de map waarin het logo zich bevindt
                    string logoPath = Path.Combine(_hostingEnvironment.WebRootPath, "images", "bull.jpg");

                    var pdfDocument = _createPDF.CreatePDFDocumentAsync(bestellings, logoPath); // wait for the task to complete

                    // Als de map pdf nog niet bestaat in de wwwroot map,
                    // maak deze dan aan voordat je het PDF-document opslaat.
                    string pdfFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "pdf");
                    Directory.CreateDirectory(pdfFolderPath);
                    //Combineer het pad naar de wwwroot map met het gewenste subpad en bestandsnaam voor het PDF-document.
                    string filePath = Path.Combine(pdfFolderPath, "example.pdf");
                    // Opslaan van de MemoryStream naar een bestand
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        pdfDocument.WriteTo(fileStream);
                    }
                    _emailSender.SendEmailAttachmentAsync(
                        mailVM.FromEmail,
                        "contact pagina",
                        mailVM.FromName,
                        pdfDocument,
                        pdfFileName
                        ).Wait(); // wait for the task to complete

                    return View("Thanks");

                }
                catch (Exception ex)
                {
                    // Log the exception here
                    ModelState.AddModelError("", "An error occurred while sending the email.");
                }

            }

            return View();
        }
    }
}
