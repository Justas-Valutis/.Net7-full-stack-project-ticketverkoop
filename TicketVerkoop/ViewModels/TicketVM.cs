namespace TicketVerkoop.ViewModels
{
    public class TicketVM
    {

        public int? Id { get; set; }
        public int MatchId { get; set; }
        public DateTime DateTime { get; set; }
        public string Datum { get; set; }
        public string DayOfWeek { get; set; }
        public string Time { get; set; }
        public string StadiumNaam { get; set; }
        public string Stad { get; set; }
        public string ThuisPloegNaam { get; set; }
        public string UitPloegNaam { get; set; }
        public string RingNaam {  get; set; }
        public int? SectionId { get; set; }
        public int aantaZitPlaatsen { get; set; }
        public string Prijs { get; set; }
        public int? BestellingId { get; set; }
        public int? StoelId { get; set; }
        public int? TicketId { get; set; }
        public List<ZitPlaatsVM>? Zitplaats { get; set; }
    }
}
