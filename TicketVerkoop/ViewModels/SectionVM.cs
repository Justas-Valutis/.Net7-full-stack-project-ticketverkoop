namespace TicketVerkoop.ViewModels
{
    public class SectionVM
    {
        public int SectionId { get; set; }
        public double Prijs { get; set; }
        public int AantalZitplaatsen { get; set; }
        public List<ZitPlaatsVM> Zitplaats { get; set; } = new List<ZitPlaatsVM>();
    }
}
