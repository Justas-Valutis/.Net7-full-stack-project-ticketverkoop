using Microsoft.AspNetCore.Mvc;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : Controller
    {

        private readonly Stadium Stadium;

        public HotelController()
        {
            Stadium = new Stadium();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetHotels()
        {
            var hotels = new[]
            {
            new HotelVM { Id = 1, Name = "Hotel ABC", Address = "123 Main St", City = Stadium.Stad.ToString(), Country = "België" },
            new HotelVM { Id = 2, Name = "Hotel DEF", Address = "456 Elm St", City = Stadium.Stad.ToString(), Country = "België" },
            
        };
            return Ok(hotels);
        }
    }
}
