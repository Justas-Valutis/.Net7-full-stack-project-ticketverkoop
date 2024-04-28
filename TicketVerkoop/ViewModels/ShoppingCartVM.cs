namespace TicketVerkoop.ViewModels
{
    public class ShoppingCartVM
    {
        public List<TicketVM>? Tickets { get; set; }
        public List<AbonnementSelectieVM>? Abonnementen { get; set; }
    }

    public class CartVM
    {
        public int MatchId { get; set; }
        public string StadiumNaam { get; set; }
        public string Stad { get; set; }
        public string ThuisPloegNaam { get; set; }
        public string UitPloegNaam { get; set; }

        public int aantaZitPlaatsen { get; set; }
        public string Prijs { get; set; }
        public System.DateTime? DateGekocht { get; set; }

    }
}
