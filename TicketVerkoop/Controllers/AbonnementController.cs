using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Extentions;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class AbonnementController : Controller
    {
        private readonly IService<Ploeg> ploegService;
        private readonly IRingService<Ring> ringService;
        private readonly IGetAllByService<Section> sectionService;
        private readonly IMapper mapper;

        public AbonnementController(IService<Ploeg> ploegService,
            IRingService<Ring> ringService,
            IGetAllByService<Section> sectionService,
            IMapper mapper)
        {
            this.ploegService = ploegService;
            this.ringService = ringService;
            this.sectionService = sectionService;
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

        public async Task<IActionResult> AbonnementSelection(int StadiumID,
                int PloegID, string PloegNaam, string StadiumNaam)
        {
            var abonnementSelectieVM = new AbonnementSelectieVM
            {
                ThuisStadiumId = StadiumID,
                StadiumNaam = StadiumNaam,
                PloegId = PloegID,
                PloegNaam = PloegNaam
            };
            try
            {
                ViewBag.lstRings = new SelectList(await ringService.GetRingsByStadiumId(Convert.ToInt16(StadiumID)), "RingId", "ZoneLocatie");


                return View(abonnementSelectieVM);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AbonnementSelection(AbonnementSelectieVM abonnementSelectieVM,
            int StadiumID, int RingId, int sectionId)
        {
            abonnementSelectieVM.Prijs = berekenPrijs(RingId, sectionId, abonnementSelectieVM);
            try
            {
                ViewBag.lstRings = new SelectList(await ringService.GetRingsByStadiumId(Convert.ToInt16(StadiumID)), "RingId", "ZoneLocatie", RingId);
                ViewBag.lstSections = new SelectList(await sectionService.GetAllBy(Convert.ToInt16(RingId)), "SectionId", "SectionId", sectionId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }
            return View(abonnementSelectieVM);
        }

        private decimal? berekenPrijs(int RingId, int sectionId,
                        AbonnementSelectieVM abonnementSelectieVM)
        {
            if (RingId == 0 || sectionId == 0)
            {
                return null;
            }
            else
            {
                abonnementSelectieVM.SelectedRingId = RingId;
                abonnementSelectieVM.SelectedSectiondId = sectionId;
                decimal prijs;
                if (RingId % 2 == 1)
                {
                    abonnementSelectieVM.SelectedRingNaam = "Bovenring";
                    prijs = 500 + sectionId * 10;
                }
                else
                {
                    abonnementSelectieVM.SelectedRingNaam = "Onderring";
                    prijs = 600 + sectionId * 10;
                }
                return prijs;
            }
        }

        [HttpPost]
        public IActionResult AddAbonnement(AbonnementSelectieVM abonnementSelectieVM)
        {
            ShoppingCartVM shopping = ShopCartHelper.GetOrCreateShoppingCart(HttpContext);
            shopping.Abonnementen.Add(abonnementSelectieVM);
            HttpContext.Session.SetObject("ShoppingCart", shopping);
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
