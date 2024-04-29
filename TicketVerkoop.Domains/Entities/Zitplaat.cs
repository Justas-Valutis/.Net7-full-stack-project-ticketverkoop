using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Zitplaat
{
    public int ZitplaatsId { get; set; }

    public int SectionId { get; set; }

    public int? TicketId { get; set; }

    public virtual ICollection<Abonnement> Abonnements { get; set; } = new List<Abonnement>();

    public virtual Section Section { get; set; } = null!;

    public virtual Ticket? Ticket { get; set; }
}
