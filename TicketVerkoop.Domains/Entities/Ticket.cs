using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int MatchId { get; set; }

    public int BestellingId { get; set; }

    public virtual Bestelling Bestelling { get; set; } = null!;

    public virtual Match Match { get; set; } = null!;

    public virtual ICollection<Zitplaat> Zitplaats { get; set; } = new List<Zitplaat>();
}
