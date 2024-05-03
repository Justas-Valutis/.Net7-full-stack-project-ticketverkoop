using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.Services.Interfaces;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    [Authorize]
    public class BookingHistoryController : Controller
    {

        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IService<Bestelling> bestellingService;

        public BookingHistoryController(IMapper mapper,
            IService<Bestelling> bestellingService,
            UserManager<IdentityUser> userManager) 
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.bestellingService = bestellingService;
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                var bestellingen = await bestellingService.GetAllByUserId(user.Id);
                if(bestellingen != null)
                {
                    List<BestelllingVM> bestellingenVM = mapper.Map<List<BestelllingVM>>(bestellingen);
                    return View(bestellingenVM);
                }
    ;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }
            return View();
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> OrderDetails(int id)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
                var bestellingen = await bestellingService.GetAllByUserId(user.Id);
                if (bestellingen != null)
                {
                    List<BestelllingVM> bestellingenVM = mapper.Map<List<BestelllingVM>>(bestellingen);
                    return View(bestellingenVM);
                }
    ;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Errorlog " + ex.Message);
            }
            return View();
        }
    }
}
