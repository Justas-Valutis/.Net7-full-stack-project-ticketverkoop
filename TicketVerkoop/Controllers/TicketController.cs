using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class TicketController : Controller
    {
        private IService<Stadium> stadiumService;
        private IGetAllByService<Section> sectionService;
        private readonly IMapper mapper;

        public TicketController(IService<Stadium> stadiumService,
            IGetAllByService<Section> sectionService,
            IMapper mapper)
        {
            this.stadiumService = stadiumService;
            this.sectionService = sectionService;
            this.mapper = mapper;
        }

  
        public async Task<IActionResult> Ticket(int? matchID, int? stadiumId, int? RingId, int? sectionId)
        {
            try
            {
                var stadium = await stadiumService.FindById(Convert.ToInt16(stadiumId));
                StadiumTicketVM stadiumTicketVM = mapper.Map<StadiumTicketVM>(stadium);
       
                ViewBag.lstRings = new SelectList(stadium.Rings, "RingId", "ZoneLocatie", RingId);
                ViewBag.lstSections = new SelectList(await sectionService.GetAllBy(Convert.ToInt16(RingId)), "SectionId", "Prijs", sectionId);

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
