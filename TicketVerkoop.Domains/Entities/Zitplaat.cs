using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Zitplaat
{
    public int ZitplaatsId { get; set; }

    public int SectionId { get; set; }

    public virtual Section Section { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
