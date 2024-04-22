namespace TicketVerkoop.ViewModels
{
    public class StadiumTicketVM
    {
        public int MatchId { get; set; }
        public int StadiumId { get; set; }

        public string? StadiumNaam { get; set; }
        public string? Stad { get; set; }
        public List<RingVM> Rings { get; set; } = new List<RingVM>();
        public List<SectionVM> Sections { get; set; } = new List<SectionVM>();
        public int SelectedRingId { get; set; }
        public string ThuisPloegNaam { get; set; } = null!;
        public string UitPloegNaam { get; set; } = null!;
    }
}
