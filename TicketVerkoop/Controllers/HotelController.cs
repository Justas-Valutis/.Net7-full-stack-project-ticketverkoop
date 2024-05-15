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

        public IActionResult Index()
        {
            return View();
        }


        //opvragen data Api (nog niet werkend) https://rapidapi.com/apidojo/api/booking
        //public async Task<IActionResult> GetHotel()
        //{
        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Get,
        //        RequestUri = new Uri("https://apidojo-booking-v1.p.rapidapi.com/locations/auto-complete?text=Brugge&languagecode=en-us"),
        //        Headers =
        //{
        //    { "X-RapidAPI-Key", "c5bfa8cf14mshd9656d1808ea2f0p1a4fb2jsn8997d6a26a17" },
        //    { "X-RapidAPI-Host", "apidojo-booking-v1.p.rapidapi.com" },
        //},
        //    };
        //    using (var response = await client.SendAsync(request))
        //    {
        //        response.EnsureSuccessStatusCode();
        //        var responseBody = await response.Content.ReadAsStringAsync();

        //        // Deserialize the JSON response into the model class
        //        var hotelData = JsonConvert.DeserializeObject<HotelVM>(responseBody);

        //        // Pass the deserialized data to the view
        //        return View(hotelData);
        //    }
        //}
    }
}
