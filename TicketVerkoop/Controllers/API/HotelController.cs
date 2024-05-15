using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketVerkoop.Domains.Entities;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers.API
{
    //[Route("api/[controller]")]
    //[ApiController]
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

        //opvragen data Api (nog niet werkend) https://rapidapi.com/apidojo/api/booking
        public async Task<IActionResult> GetHotel()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://apidojo-booking-v1.p.rapidapi.com/properties/v2/get-rooms?hotel_id=1720410&departure_date=2022-10-10&arrival_date=2022-10-5&rec_guest_qty=2&rec_room_qty=1&currency_code=USD&languagecode=en-us&units=imperial"),
                Headers =
        {
            { "X-RapidAPI-Key", "c5bfa8cf14mshd9656d1808ea2f0p1a4fb2jsn8997d6a26a17" },
            { "X-RapidAPI-Host", "apidojo-booking-v1.p.rapidapi.com" },
        },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response into the model class
                var hotelData = JsonConvert.DeserializeObject<HotelVM>(responseBody);

                // Pass the deserialized data to the view
                return View(hotelData);
            }
        }
    }
}
