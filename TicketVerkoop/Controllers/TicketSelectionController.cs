using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Extentions;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class TicketSelectionController : Controller
    {
        private IMatchService<Match> matchService;
        private IGetAllByService<Section> sectionService;
        private readonly IMapper mapper;

        public TicketSelectionController(IMatchService<Match> matchService,
            IGetAllByService<Section> sectionService,
            IMapper mapper)
        {
            this.matchService = matchService;
            this.sectionService = sectionService;
            this.mapper = mapper;
        }


        public async Task<IActionResult> TicketSelection(int matchID, int? RingId, int? sectionId, string? chosenSeatNr)
        {
            try
            {
                var match = await matchService.FindById(Convert.ToInt16(matchID));
                StadiumTicketVM stadiumTicketVM = mapper.Map<StadiumTicketVM>(match);

                int? chosenSeats = null;
                if (!string.IsNullOrEmpty(chosenSeatNr) && int.TryParse(chosenSeatNr, out int parsedSeatNr))
                {
                    stadiumTicketVM.chosenSeatNr = parsedSeatNr;
                    if (sectionId != null && RingId != null && stadiumTicketVM.Sections?.Count > 0)  {
                        stadiumTicketVM.TotalePrijs = Math.Round(parsedSeatNr * stadiumTicketVM.Sections.FirstOrDefault(s => s.SectionId == sectionId).Prijs, 2).ToString("N2");
                    }
                    else
                    {
                        stadiumTicketVM.TotalePrijs = null;
                    }
                }

                ViewBag.lstRings = new SelectList(stadiumTicketVM.Rings, "RingId", "ZoneLocatie", RingId);
                ViewBag.lstSections = new SelectList(await sectionService.GetAllBy(Convert.ToInt16(RingId)), "SectionId", "SectionId", sectionId);
                List<int> numbers = new List<int> { 1, 2, 3, 4 };
                ViewBag.kiesAantalZitplaaatsen = new SelectList(numbers, selectedValue: stadiumTicketVM.chosenSeatNr);

                return View(stadiumTicketVM);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }
            return View();
        }

        public IActionResult AddTicket(int MatchId, string StadiumNaam, string Stad, 
            string ThuisPloegNaam, string UitPloegNaam, int aantalZitPlaatsen, string Prijs)
        {
            var CartVM = new CartVM
            {
                MatchId = MatchId,
                StadiumNaam = StadiumNaam,
                Stad = Stad,
                ThuisPloegNaam = ThuisPloegNaam,
                UitPloegNaam = UitPloegNaam,
                Prijs = Prijs,
                aantaZitPlaatsen = aantalZitPlaatsen,
                DateCreated = DateTime.Now
            };

            try { 
            HttpContext.Session.SetObject("mySession",
                new SessionVM { Date = DateTime.Now, Company = "Vives" });
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("SESSION" + ex.Message);
            }
            ShoppingCartVM? shopping;

            if (HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            }
            else
            {
                shopping = new ShoppingCartVM();
                shopping.Cart = new List<CartVM>();
            }
            shopping?.Cart?.Add(CartVM);
            HttpContext.Session.SetObject("ShoppingCart", shopping);
            return RedirectToAction("Index", "ShoppingCart");
            //return View(CartVM);

        }

    }
}
