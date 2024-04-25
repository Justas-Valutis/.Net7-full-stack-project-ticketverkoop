using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class AbonnementController : Controller
    {
        private readonly IService<Ploeg> ploegService;
        private readonly IMapper mapper;

        public AbonnementController(IService<Ploeg> ploegService, IMapper mapper)
        {
            this.ploegService = ploegService;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var ploegen = await ploegService.GetAll();
                List<PloegVM> ploegenVM = mapper.Map<List<PloegVM>>(ploegen);

                return View(ploegenVM);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }
            return View();
        }
    }
}
