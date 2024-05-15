using TicketVerkoop.Repositories;

namespace TicketVerkoop.ViewModels
{
    public class StadiumVM
    {
        public int StadiumId { get; set; }

        public string? Naam { get; set; }
        public string? Stad { get; set; }
        public int? Capaciteit { get; set; }
    }
}
