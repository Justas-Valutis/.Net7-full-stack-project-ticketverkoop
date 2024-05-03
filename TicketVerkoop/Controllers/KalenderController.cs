using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class KalenderController : Controller
    {

        private IMatchService<Match> matchService;
        private IService<Stadium> stadiumService;
        private IService<Ploeg> ploegService;
        private readonly IMapper mapper;

        public KalenderController(IMatchService<Match> matchService,
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
            try
            {
                ViewBag.lstStadiums = new SelectList(await stadiumService.GetAll(), "StadiumId", "Naam");
                ViewBag.lstPloegen = new SelectList(await ploegService.GetAll(), "PloegId", "Naam");
                ViewBag.EndDate = DateTime.Now;

                var list = await matchService.GetAll();
                List<MatchVM> listVM = mapper.Map<List<MatchVM>>(list);
                listVM.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));
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
            if (StadiumId == null && PloegId == null)
            {
                return await Index();
            }
            List<MatchVM> sortedMatches = new List<MatchVM>();

            try
            {
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

                sortedMatches.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));
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
