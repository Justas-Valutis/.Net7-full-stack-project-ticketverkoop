using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using System.Diagnostics;
using TicketVerkoop.Data;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    [Authorize]
    public class BookingHistoryController : Controller
    {

        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IService<Bestelling> bestellingService;
        private readonly IBasketService<Ticket> ticketService;
        private readonly IBasketService<Abonnement> abonnementService;
        private readonly IStoelService<Zitplaat> stoelService;


        public BookingHistoryController(IMapper mapper,
            IService<Bestelling> bestellingService,
            UserManager<ApplicationUser> userManager,
            IBasketService<Ticket> ticketService,
            IBasketService<Abonnement> abonnementService,
            IStoelService<Zitplaat> stoelService) 
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.bestellingService = bestellingService;
            this.ticketService = ticketService;
            this.abonnementService = abonnementService;
            this.stoelService = stoelService;
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

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> OrderDetails(int sectionId, int zitPlaatsId, int BestellingId)
        {
            try
            {
                await stoelService.DeleteZitplaats(Convert.ToInt16(sectionId), Convert.ToInt16(zitPlaatsId));
                BestellingenVM bestellingenVM = new BestellingenVM();
                bestellingenVM.BestellingId = Convert.ToInt16(BestellingId);
                bestellingenVM.TotalPrijs = bestellingService.FindById(Convert.ToInt16(BestellingId)).Result.TotalPrijs;
                await OrderDetails(bestellingenVM);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }
            return View();

        }
    }
}
