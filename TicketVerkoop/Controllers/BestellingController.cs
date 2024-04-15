using Microsoft.AspNetCore.Mvc;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class BestellingController : Controller
    {
        public IActionResult Index()
        {
            //BestellingVM? cartList = HttpContext.Session.GetObject<BestellingVM>("ShoppingCart"); 
            return View();//cartList);
        }
    }
}
