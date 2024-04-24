using System.ComponentModel.DataAnnotations;

namespace TicketVerkoop.ViewModels
{
    public class MailVM
    {
        [Required, Display(Name = "Jouw naam")]
        public string? FromName { get; set; }

        [Required, Display(Name = "Jouw email")]
        public string? FromEmail { get; set; }
    }
}
