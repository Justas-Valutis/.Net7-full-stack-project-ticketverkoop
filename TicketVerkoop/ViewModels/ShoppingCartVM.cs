namespace TicketVerkoop.ViewModels
{
    public class ShoppingCartVM
    {
        public List<TicketVM>? Tickets { get; set; }
        public List<AbonnementSelectieVM>? Abonnementen { get; set; }
        public System.DateTime? DateGekocht { get; set; }

        public bool MailSent { get; set; }
    }
}
