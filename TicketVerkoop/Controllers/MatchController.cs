using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class MatchController : Controller
    {
        private IService<Match> matchService;
        private readonly IMapper mapper;

        public MatchController(IService<Match> matchService, IMapper mapper)
        {
            this.matchService = matchService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var list = await matchService.GetAll();
            List<MatchVM> listVM = mapper.Map<List<MatchVM>>(list);
            return View(listVM);
        }
    }
}
