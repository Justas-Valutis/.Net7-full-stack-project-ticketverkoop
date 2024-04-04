﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class KalenderController : Controller
    {

        private IService<Match> matchService;
        private IService<Stadium> stadiumService;
        private IService<Ploeg> ploegService;
        private readonly IMapper mapper;

        public KalenderController(IService<Match> matchService,
            IService<Stadium> stadiumService,
            IService<Ploeg> ploegService,
            IMapper mapper)
        {
            this.matchService = matchService;
            this.stadiumService = stadiumService;
            this.ploegService = ploegService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.lstStadiums = new SelectList(await stadiumService.GetAll(), "StadiumId", "Naam");
            ViewBag.lstPloegen = new SelectList(await ploegService.GetAll(), "PloegId", "Naam");
            ViewBag.EndDate = DateTime.Now;
            try
            {
                var list = await matchService.GetAll();
                List<MatchVM> listVM = mapper.Map<List<MatchVM>>(list);
                return View(listVM);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? StadiumId, int? PloegId)
        {
            try
            {
                if (StadiumId == null && PloegId == null)
                {
                return await Index();
                }

                List<MatchVM> sortedMatches = new List<MatchVM>();
                if (StadiumId != null && PloegId != null)
                {
                    var listPloeg = await matchService.GetMatchByPloegIdAndStadiumId(Convert.ToInt16(PloegId), Convert.ToInt16(StadiumId));
                    sortedMatches = mapper.Map<List<MatchVM>>(listPloeg);
                    ViewBag.lstPloegen = new SelectList(await ploegService.GetAll(), "PloegId", "Naam", PloegId);
                    ViewBag.lstStadiums = new SelectList(await stadiumService.GetAll(), "StadiumId", "Naam", StadiumId);
                }
                else if (StadiumId != null)
                {
                // ---- Sort by Stadium ----
                var listStadium = await matchService.GetMatchByStadiumId(Convert.ToInt16(StadiumId));
                sortedMatches = mapper.Map<List<MatchVM>>(listStadium);
                ViewBag.lstStadiums = new SelectList(await stadiumService.GetAll(), "StadiumId", "Naam", StadiumId);
                ViewBag.lstPloegen = new SelectList(await ploegService.GetAll(), "PloegId", "Naam", PloegId);
                }
                else if (PloegId != null)
                {
                // ---- Sort by Ploeg ----
                var listPloeg = await matchService.GetMatchByPloegId(Convert.ToInt16(PloegId));
                sortedMatches = mapper.Map<List<MatchVM>>(listPloeg);
                ViewBag.lstPloegen = new SelectList(await ploegService.GetAll(), "PloegId", "Naam", PloegId);
                ViewBag.lstStadiums = new SelectList(await stadiumService.GetAll(), "StadiumId", "Naam", StadiumId);
                }


                return View(sortedMatches);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
                return View();
            }
        }
    }
}
