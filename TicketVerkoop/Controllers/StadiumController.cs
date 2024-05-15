using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class StadiumController : Controller
    {
        private IService<Stadium> stadiumService;
        private IRingService<Ring> ringService;
        private readonly IMapper mapper;

        public StadiumController(IService<Stadium> stadiumService,
            IRingService<Ring> ringService,
            IMapper mapper)
        {
            this.stadiumService = stadiumService;
            this.ringService = ringService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(int stadiumID)
        {
            try
            {
                var stadium = await stadiumService.FindById(Convert.ToInt16(stadiumID));
                StadiumVM stadiumVM = mapper.Map<StadiumVM>(stadium);
                stadiumVM.Capaciteit = await ringService.GetStadiumCapacity(Convert.ToInt16(stadiumID));
                return View(stadiumVM);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }
            return View();
        }

        public async Task<IActionResult> IndexStadium()
        {
            try
            {
                var list = await stadiumService.GetAll();
                List<StadiumVM> listVM = mapper.Map<List<StadiumVM>>(list);
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
