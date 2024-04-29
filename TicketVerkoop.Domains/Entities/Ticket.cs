using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int MatchId { get; set; }

    public int ZitplaatsId { get; set; }

    public string RingNaam { get; set; } = null!;

    public int SectionId { get; set; }

    public int StoelenBesteld { get; set; }

    public decimal Prijs { get; set; }

    public virtual ICollection<Bestelling> Bestellings { get; set; } = new List<Bestelling>();

    public virtual Match Match { get; set; } = null!;

    public virtual Zitplaat Zitplaats { get; set; } = null!;
}
