using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Abonnement
{
    public int AbonnementId { get; set; }

    public int ZitplaatsId { get; set; }

    public int PloegId { get; set; }

    public decimal Prijs { get; set; }

    public string PloegNaam { get; set; } = null!;

    public string StadiaNaam { get; set; } = null!;

    public string RingNaam { get; set; } = null!;

    public int SectionId { get; set; }

    public virtual ICollection<Bestelling> Bestellings { get; set; } = new List<Bestelling>();
}
