using Microsoft.AspNetCore.Mvc;
using TicketVerkoop.Extentions;
using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            //call SessionID
            //var SessiondId = HttpContext.Session.Id;
            return View(cartList);
        }
    }
}
