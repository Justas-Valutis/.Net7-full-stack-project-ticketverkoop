using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Abonnement
{
    public int AbonnementId { get; set; }

    public int ZitplaatsId { get; set; }

    public int PloegId { get; set; }

    public double Prijs { get; set; }

    public virtual ICollection<Bestelling> Bestellings { get; set; } = new List<Bestelling>();

    public virtual Ploeg Ploeg { get; set; } = null!;

    public virtual Zitplaat Zitplaats { get; set; } = null!;
}
