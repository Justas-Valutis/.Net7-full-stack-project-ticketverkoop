using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class PloegController : Controller
    {
        private IService<Ploeg> ploegService;
        private readonly IMapper mapper;

        public PloegController(IService<Ploeg> ploegService, IMapper mapper)
        {
            this.ploegService = ploegService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.EndDate = DateTime.Now;

                var list = await ploegService.GetAll();
                List<PloegVM> listVM = mapper.Map<List<PloegVM>>(list);
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
