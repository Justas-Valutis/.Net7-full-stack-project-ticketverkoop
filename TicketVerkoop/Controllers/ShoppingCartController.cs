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
        private readonly IBasketService<Abonnement> abonnementService;
        private readonly IStoelService<Zitplaat> stoelService;
        private readonly IBasketService<Ticket> ticketService;


        public ShoppingCartController(IEmailSend emailSend,
            ICreatePDF createPDF, 
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper,
            IService<Bestelling> bestellingService,
            IBasketService<Abonnement> abonnementService,
            IStoelService<Zitplaat> stoelService,
            IBasketService<Ticket> ticketService)
        {
            _createPDF = createPDF;
            _emailSender = emailSend;
            _hostingEnvironment = webHostEnvironment;
            this.mapper = mapper;
            this.bestellingService = bestellingService;
            this.abonnementService = abonnementService;
            this.stoelService = stoelService;
            this.ticketService = ticketService;
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
        public async Task<IActionResult> Checkout()
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
                //---------------    Bestelling toevoegen in database  ---------------------------------------
                var bestelling = mapper.Map<Bestelling>(bestellingVM);
                var bestellingID = Convert.ToInt16(await bestellingService.AddandGetID(bestelling));
                //---------------    Abonnementen toevoegen in database ---------------------------------------
                if (shoppingCartVM.Abonnementen != null && shoppingCartVM.Abonnementen.Count > 0)
                {
                    shoppingCartVM.Abonnementen.ForEach(x =>
                    {
                        x.BestellingId = bestellingID;
                    });
                    var abonnementen = mapper.Map<List<Abonnement>>(shoppingCartVM.Abonnementen);
                    var listAabonnementenIds = await abonnementService.AddListAndGetIDs(abonnementen);

                    //Toevoegen zitplaats
                    for (int i = 0; i < shoppingCartVM.Abonnementen.Count(); i++)
                    {
                        shoppingCartVM.Abonnementen[i].AbonnementId = listAabonnementenIds[i];
                    }

                    var zitPlaatsen = mapper.Map<List<Zitplaat>>(shoppingCartVM.Abonnementen);
                    var listStoelenIds = await stoelService.ReserveerStoelen(zitPlaatsen);

                    for (int i = 0; i < shoppingCartVM.Abonnementen.Count(); i++)
                    {
                        shoppingCartVM.Abonnementen[i].StoelId = listStoelenIds[i];
                    }
                }

                //---------------    Tickets toevoegen in database ---------------------------------------
                if (shoppingCartVM.Tickets != null && shoppingCartVM.Tickets.Count > 0)
                {
                    shoppingCartVM.Tickets.ForEach(x =>
                    {
                        x.BestellingId = bestellingID;
                    });
                    var tickets = mapper.Map<List<Ticket>>(shoppingCartVM.Tickets);
                    var listTicketsIds = await ticketService.AddListAndGetIDs(tickets);

                    //Toevoegen zitplaats
                    for (int i = 0; i < shoppingCartVM.Tickets.Count(); i++)
                    {
                        shoppingCartVM.Tickets[i].TicketId = listTicketsIds[i];
                    }

                    var zitPlaatsen = mapper.Map<List<Zitplaat>>(shoppingCartVM.Tickets);
                    var listStoelenIds = await stoelService.ReserveerStoelen(zitPlaatsen);

                    for (int i = 0; i < shoppingCartVM.Tickets.Count(); i++)
                    {
                        shoppingCartVM.Tickets[i].StoelId = listStoelenIds[i];
                    }
                }
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }

            HttpContext.Session.SetObject("ShoppingCart", null);
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

