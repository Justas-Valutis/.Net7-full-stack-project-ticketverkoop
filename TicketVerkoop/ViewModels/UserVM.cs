namespace TicketVerkoop.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }

        public string? UserName { get; set; }

        public string? NormalizedUserName { get; set; }

        public string? Email { get; set; }

        public string? NormalizedEmail { get; set; }

        public bool EmailConfirmed { get; set; }

        public string? SecurityStamp { get; set; }

        public string? ConcurrencyStamp { get; set; }

        public string? PhoneNumber { get; set; }
        public List<BestellingenVM>? Bestellings { get; set; }

    }
}
