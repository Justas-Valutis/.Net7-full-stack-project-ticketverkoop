using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    [Authorize]
    public class BookingHistoryController : Controller
    {

        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IService<Bestelling> bestellingService;
        private readonly IBasketService<Ticket> ticketService;
        private readonly IBasketService<Abonnement> abonnementService;

        public BookingHistoryController(IMapper mapper,
            IService<Bestelling> bestellingService,
            UserManager<IdentityUser> userManager,
            IBasketService<Ticket> ticketService,
            IBasketService<Abonnement> abonnementService) 
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.bestellingService = bestellingService;
            this.ticketService = ticketService;
            this.abonnementService = abonnementService;
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                var bestellingen = await bestellingService.GetAllByUserId(user.Id);
                if(bestellingen != null)
                {
                    List<BestellingenVM> bestellingenVM = mapper.Map<List<BestellingenVM>>(bestellingen).OrderByDescending(b => b.BestelDatum).ToList();
                    return View(bestellingenVM);
                }
    ;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }
            return View();
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> OrderDetails(BestellingenVM item)
        {
            try
            {
                var tickets = await ticketService.GetAllByBestellingId(Convert.ToInt16(item.BestellingId)) ;
                if (tickets != null && tickets.Count() > 0)
                {
                    List<TicketVM> ticketVMs = mapper.Map<List<TicketVM>>(tickets);
                    item.Tickets = ticketVMs;
                }

                var abonnementen = await abonnementService.GetAllByBestellingId(Convert.ToInt16(item.BestellingId));

                if (abonnementen != null && abonnementen.Count() > 0)
                {
                    List<AbonnementSelectieVM> abonnementenVM = mapper.Map<List<AbonnementSelectieVM>>(abonnementen);
                    item.Abonnements = abonnementenVM;
                }

                return View(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }
            return View();
        }
    }
}
