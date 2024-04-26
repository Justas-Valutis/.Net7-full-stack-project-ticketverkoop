using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using TicketVerkoop.Models;

namespace TicketVerkoop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult setAppLanguage(string lang, string returnUrl)
        //{
        //    Response.Cookies.Append(
        //            CookieRequestCultureProvider.DefaultCookieName,
        //            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
        //            new CookieOptions { Expires = System.DateTimeOffset.UtcNow.AddYears(1) }
        //        );

        //    return LocalRedirect(returnUrl);
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
