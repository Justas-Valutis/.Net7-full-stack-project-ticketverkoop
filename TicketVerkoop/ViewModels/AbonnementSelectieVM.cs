namespace TicketVerkoop.ViewModels
{
    public class AbonnementSelectieVM
    {
        public int Id { get; set; }
        public int PloegId { get; set; }

        public string? PloegNaam { get; set; }

        public string StadiumNaam { get; set; }

        public int ThuisStadiumId { get; set; }
        public decimal? Prijs {  get; set; }
        public int? SelectedRingId { get; set; }
        public string? SelectedRingNaam { get; set; }
        public int? SelectedSectiondId { get; set; }
        public int? BestellingId { get; set; }
        public int? AbonnementId { get; set; }
        public int? StoelId { get; set; }
    }
}
