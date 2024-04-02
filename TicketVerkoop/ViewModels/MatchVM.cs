namespace TicketVerkoop.ViewModels
{
    public class MatchVM
    {
        public int MatchId { get; set; }
        public int StadiumId { get; set; }
        public int PloegThuisId { get; set; }
        public int PloegUitId { get; set; }
        public DateTime Datum { get; set; }
        public string StadiumNaam { get; set; } = null!;
        public string ThuisPloegNaam { get; set; } = null!;
        public string UitPloegNaam { get; set; } = null!;


    }
}
