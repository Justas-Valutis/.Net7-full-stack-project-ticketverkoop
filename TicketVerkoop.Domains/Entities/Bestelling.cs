using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Bestelling
{
    public int BestellingId { get; set; }

    public int? TicketId { get; set; }

    public int? AbonnementId { get; set; }

    public int UserId { get; set; }

    public DateTime BestelDatum { get; set; }

    public virtual Abonnement? Abonnement { get; set; }

    public virtual Ticket? Ticket { get; set; }
}
