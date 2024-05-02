using TicketVerkoop.Domains.Entities;

namespace TicketVerkoop.ViewModels
{
    public class BestellingenVM
    {
        public List<BestellingenVM> Bestellingen { get; set; }
    }

    public class BestelllingVM
    {
        public int? BestellingId { get; set; }

        public int? AbonnementId { get; set; }

        public string UserId { get; set; }

        public DateTime BestelDatum { get; set; }
    }
}
