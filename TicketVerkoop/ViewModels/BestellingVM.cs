using TicketVerkoop.Domains.Entities;

namespace TicketVerkoop.ViewModels
{
    public class BestellingVM
    {
        public List<Bestelling> Bestellingen { get; set; }
    }

    public class Bestelling
    {
        public int BestellingId { get; set; }

        public int? TicketId { get; set; }

        public int? AbonnementId { get; set; }

        public int UserId { get; set; }

        public DateTime BestelDatum { get; set; }
    }
}
