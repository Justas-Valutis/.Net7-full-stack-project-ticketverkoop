using TicketVerkoop.Domains.Entities;

namespace TicketVerkoop.ViewModels
{
    public class BestellingenVM
    {
        public List<BestellingenVM> Bestellingen { get; set; }
    }

    public class BestelllingVM
    {
        public int BestellingId { get; set; }

        public int? TicketId { get; set; }

        public int? AbonnementId { get; set; }

        public int UserId { get; set; }

        public DateTime BestelDatum { get; set; }
    }
}
