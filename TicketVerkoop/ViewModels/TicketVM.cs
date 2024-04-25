namespace TicketVerkoop.ViewModels
{
    public class TicketVM
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
