namespace TicketVerkoop.ViewModels
{
    public class StadiumTicketVM
    {
        public int StadiumId { get; set; }

        public string? Naam { get; set; }
        public string? Stad { get; set; }
        public List<RingVM> Rings { get; set; } = new List<RingVM>();
        public int SelectedRingId { get; set; }
    }
}
