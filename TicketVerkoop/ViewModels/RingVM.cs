using TicketVerkoop.Repositories;

namespace TicketVerkoop.ViewModels
{
    public class RingVM
    {
        public int RingId { get; set; }
        public string ZoneLocatie { get; set; }
        public List<SectionVM> Sections { get; set; } = new List<SectionVM>();
    }
}
