﻿using AutoMapper;
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
                stadiumTicketVM.TotalePrijs = null;
                int? chosenSeats = null;
                if (!string.IsNullOrEmpty(chosenSeatNr) && int.TryParse(chosenSeatNr, out int parsedSeatNr))
                {
                    stadiumTicketVM.SelectedRingNaam = RingId % 2 == 1 ? "Bovenring" : "Onderring";
                    stadiumTicketVM.chosenSeatNr = parsedSeatNr;
                    if (sectionId != null && RingId != null)  {
                        stadiumTicketVM.SelectedSectionId = sectionId;
                        stadiumTicketVM.TotalePrijs = Math.Round(parsedSeatNr * stadiumTicketVM.Sections.FirstOrDefault(s => s.SectionId == sectionId).Prijs, 2).ToString("N2");
                    }
                    else
                    {
                        stadiumTicketVM.TotalePrijs = null;
                    }
                }

                ViewBag.lstRings = new SelectList(stadiumTicketVM.Rings, "RingId", "ZoneLocatie", RingId);
                ViewBag.lstSections = new SelectList(await sectionService.GetAllBy(Convert.ToInt16(RingId)), "SectionId", "SectionId", sectionId);
                List<int> numbers = new List<int> { 1 };
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
            string ThuisPloegNaam, string UitPloegNaam, int aantalZitPlaatsen, string Prijs,
            string RingNaam, int SectionId, string Datum, string DayOfWeek, string Time)
        {
            ShoppingCartVM shopping = ShopCartHelper.GetOrCreateShoppingCart(HttpContext);

            if (shopping.Tickets.Count() > 3)
            {
                return View("Fout");
            }

            var ticketVM = new TicketVM
            {
                Id = shopping.Tickets.Count(),
                MatchId = MatchId,
                StadiumNaam = StadiumNaam,
                Stad = Stad,
                ThuisPloegNaam = ThuisPloegNaam,
                UitPloegNaam = UitPloegNaam,
                RingNaam = RingNaam,
                SectionId = SectionId,
                Prijs = Prijs,
                aantaZitPlaatsen = aantalZitPlaatsen,
                Datum = Datum,
                DayOfWeek = DayOfWeek,
                Time = Time,
            };

            if (shopping.Tickets.Count > 0)
            {
                foreach (var item in shopping.Tickets)
                {
                    if (ticketVM.Datum == item.Datum && item.MatchId != ticketVM.MatchId)
                    {
                        return View("Fout");
                    }
                }
            }

            shopping.Tickets.Add(ticketVM);
            shopping.TotalPrijs += decimal.Parse(Prijs);
        
            HttpContext.Session.SetObject("ShoppingCart", shopping);
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
