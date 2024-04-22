using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
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

  
        public async Task<IActionResult> TicketSelection(int matchID, int? RingId, int? sectionId)
        {
            try
            { 
                var match = await matchService.FindById(Convert.ToInt16(matchID));
                StadiumTicketVM stadiumTicketVM = mapper.Map<StadiumTicketVM>(match);
       
                ViewBag.lstRings = new SelectList(stadiumTicketVM.Rings, "RingId", "ZoneLocatie", RingId);
                ViewBag.lstSections = new SelectList(await sectionService.GetAllBy(Convert.ToInt16(RingId)), "SectionId", "SectionId", sectionId);

                return View(stadiumTicketVM);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }
            return View();
        }
    }
}
