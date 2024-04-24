using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class StadiumController : Controller
    {
        private IService<Stadium> stadiumService;
        private readonly IMapper mapper;

        public StadiumController(IService<Stadium> stadiumService,
            IMapper mapper)
        {
            this.stadiumService = stadiumService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(int stadiumID)
        {
            var stadium = await stadiumService.FindById(Convert.ToInt16(stadiumID));
            StadiumVM stadiumVM = mapper.Map<StadiumVM>(stadium);
            return View(stadiumVM);
        }
    }
}
