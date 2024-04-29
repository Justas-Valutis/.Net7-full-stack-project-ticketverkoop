using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Bestelling
{
    public int BestellingId { get; set; }

    public int UserId { get; set; }

    public DateTime BestelDatum { get; set; }

    public virtual ICollection<Abonnement> Abonnements { get; set; } = new List<Abonnement>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
