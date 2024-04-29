using TicketVerkoop.ViewModels;

namespace TicketVerkoop.Extentions
{
    public static class ShopCartHelper
    {
        public static ShoppingCartVM GetOrCreateShoppingCart(HttpContext httpContext)
        {
            ShoppingCartVM shopping;
            if (httpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") != null)
            {
                shopping = httpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            }
            else
            {
                shopping = InitializeShoppingCart();
            }
            return shopping;
        }

        public static ShoppingCartVM InitializeShoppingCart()
        {
            return new ShoppingCartVM
            {
                Abonnementen = new List<AbonnementSelectieVM>(),
                Tickets = new List<TicketVM>()
            };
        }
    }
}
