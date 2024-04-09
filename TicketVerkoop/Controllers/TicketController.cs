using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class TicketController : Controller
    {
        private IService<Stadium> stadiumService;
        private readonly IMapper mapper;

        public TicketController(IService<Stadium> stadiumService, IMapper mapper)
        {
            this.stadiumService = stadiumService;
            this.mapper = mapper;
        }


        public async Task<IActionResult> Ticket(int? matchID, int? stadiumId)
        {
            if (matchID != null && stadiumId != null)
            {
                try
                {
                    var stadium = await stadiumService.FindById(Convert.ToInt16(stadiumId));
                    List<StadiumVM> stadiumVM = mapper.Map<List<StadiumVM>>(stadium);
                    return View(stadiumVM);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Errorlog " + ex.Message);
                }
            }
            return View();
        }
    }
}
