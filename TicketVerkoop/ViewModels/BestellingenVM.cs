using TicketVerkoop.Domains.Entities;

namespace TicketVerkoop.ViewModels
{
    public class BestellingenVM
    {
        public int BestellingId { get; set; }
        public List<AbonnementSelectieVM> Abonnements { get; set; } = new List<AbonnementSelectieVM>();

        public List<TicketVM> Tickets { get; set; } = new List<TicketVM>();

        public DateTime BestelDatum { get; set; }
        public decimal? TotalPrijs { get; set; }

    }

    public class BestelllingVM
    {
        public int? BestellingId { get; set; }

        public int? AbonnementId { get; set; }

        public string UserId { get; set; }

        public DateTime BestelDatum { get; set; }
        public decimal? TotalPrijs { get; set;}
    }
}
