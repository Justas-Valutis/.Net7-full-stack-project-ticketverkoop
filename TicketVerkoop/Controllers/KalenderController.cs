using AutoMapper;
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
        private readonly IMapper mapper;

        public KalenderController(IService<Match> matchService,
            IService<Stadium> stadiumService,
            IMapper mapper)
            {
            this.matchService = matchService;
            this.stadiumService = stadiumService;
            this.mapper = mapper;
            }

        public async Task<IActionResult> Index()
            {
            ViewBag.lstStadiums = new SelectList(await stadiumService.GetAll(), "StadiumId", "Naam");
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
        public async Task<IActionResult> Index(int? StadiumId)
            {
            if (StadiumId == null)
                {
                return await Index();
                }

            try
                {
                var list = await matchService.GetMatchByStadiumId(Convert.ToInt16(StadiumId));
                ViewBag.lstStadiums = new SelectList(await stadiumService.GetAll(), "StadiumId", "Naam", StadiumId);
                List<MatchVM> listVM = mapper.Map<List<MatchVM>>(list);
                return View(listVM);
                }
            catch (Exception ex)
                {
                Debug.WriteLine("Errorlog " + ex.Message);
                }
            return View();
            }
        }
    }
