using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Abonnement
{
    public int AbonnementId { get; set; }

    public int ZitplaatsId { get; set; }

    public int PloegId { get; set; }

    public decimal Prijs { get; set; }

    public int BestellingId { get; set; }

    public virtual Bestelling Bestelling { get; set; } = null!;
}
