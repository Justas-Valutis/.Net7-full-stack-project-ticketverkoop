﻿using Microsoft.AspNetCore.Mvc;
using TicketVerkoop.Util.Mail.Interfaces;
using TicketVerkoop.Util.PDF.Interfaces;
using TicketVerkoop.ViewModels;
using TicketVerkoop.Domains;
using TicketVerkoop.Extentions;
using TicketVerkoop.Domains.Entities;
using AutoMapper;
using TicketVerkoop.Services.Interfaces;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using TicketVerkoop.Services;
using Microsoft.AspNetCore.Http;
using TicketVerkoop.Data;

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
        private readonly UserManager<ApplicationUser> userManager;


        public ShoppingCartController(IEmailSend emailSend,
            ICreatePDF createPDF, 
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper,
            IService<Bestelling> bestellingService,
            IBasketService<Abonnement> abonnementService,
            IStoelService<Zitplaat> stoelService,
            IBasketService<Ticket> ticketService,
            UserManager<ApplicationUser> userManager)
        {
            _createPDF = createPDF;
            _emailSender = emailSend;
            _hostingEnvironment = webHostEnvironment;
            this.mapper = mapper;
            this.bestellingService = bestellingService;
            this.abonnementService = abonnementService;
            this.stoelService = stoelService;
            this.ticketService = ticketService;
            this.userManager = userManager;
        }

        // GET: ShoppingCartController
        [HttpGet]
        public IActionResult Index()
        {
            ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            //call SessionID
            //var SessiondId = HttpContext.Session.Id;
            return View(cartList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });

            }
            else {
                ShoppingCartVM? shoppingCartVM = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

                var bestellingVM = new BestelllingVM
                {
                    AbonnementId = 1,
                    UserId = user.Id,
                    BestelDatum = DateTime.Now,
                    TotalPrijs = shoppingCartVM.TotalPrijs
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

                    BestellingenVM bestelllingVM = new BestellingenVM
                    {
                        BestellingId = bestellingID,
                        BestelDatum = bestellingVM.BestelDatum,
                        TotalPrijs = bestellingVM.TotalPrijs
                    };

                    //---------------    Email versturen ---------------------------------------
                    string pdfFile = "Ticket" + DateTime.Now.Year;
                    var pdfFileName = $"{pdfFile}_{Guid.NewGuid()}.pdf";
                    //het pad naar de map waarin het logo zich bevindt
                    string logoPath = Path.Combine(_hostingEnvironment.WebRootPath, "images", "bull.jpg");

                    Bestelling bestellingRep = await bestellingService.FindById(Convert.ToInt16(bestellingID));

                    var pdfDocument = _createPDF.CreatePDFDocumentAsync(bestellingRep, logoPath); // wait for the task to complete
                     
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
                    _emailSender.SendEmailAttachmentAsync(user.Email, "Bestelling Ticket", "thank you for buying", pdfDocument, pdfFileName);

                    HttpContext.Session.SetObject("ShoppingCart", null);
                    return RedirectToAction("OrderDetails", "BookingHistory", bestelllingVM);
                }
                catch (Exception ex) 
                {
                    Debug.WriteLine("Errorlog " + ex.Message);
                }

                return View("Oops");
            }
        }

        [HttpPost]
        public IActionResult DeleteAbonnementItem(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            ShoppingCartVM shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            var itemToDelete = shopping.Abonnementen.FirstOrDefault(item => item.Id == id);
            if (itemToDelete == null)
            {
                return NotFound();
            }
            UpdatePrijs(itemToDelete.Prijs, shopping);
            shopping.Abonnementen.Remove(itemToDelete);

            HttpContext.Session.SetObject("ShoppingCart", shopping);
            return RedirectToAction("Index", "ShoppingCart");
        }

        [HttpPost]
        public IActionResult DeleteTicketItem(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            ShoppingCartVM shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            var itemToDelete = shopping.Tickets.FirstOrDefault(item => item.Id == id);
            if (itemToDelete == null)
            {
                return NotFound();
            }
            UpdatePrijs(decimal.Parse(itemToDelete.Prijs), shopping);
            shopping.Tickets.Remove(itemToDelete);

            HttpContext.Session.SetObject("ShoppingCart", shopping);
            return RedirectToAction("Index", "ShoppingCart");
        }

        private void UpdatePrijs(decimal? prijs, ShoppingCartVM shopping)
        {
            shopping.TotalPrijs -= prijs.Value;
        }
    }
}

