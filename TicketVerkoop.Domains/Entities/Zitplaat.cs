using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Zitplaat
{
    public int ZitplaatsId { get; set; }

    public int SectionId { get; set; }

    public int? TicketId { get; set; }

    public int? AbonnementId { get; set; }

    public virtual Abonnement? Abonnement { get; set; }

    public virtual Section Section { get; set; } = null!;

    public virtual Ticket? Ticket { get; set; }
}
