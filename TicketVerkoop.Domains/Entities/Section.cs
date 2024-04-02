using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Section
{
    public int SectionId { get; set; }

    public int RingId { get; set; }

    public double Prijs { get; set; }

    public int AantalZitplaatsen { get; set; }

    public virtual Ring Ring { get; set; } = null!;

    public virtual ICollection<Zitplaat> Zitplaats { get; set; } = new List<Zitplaat>();
}
