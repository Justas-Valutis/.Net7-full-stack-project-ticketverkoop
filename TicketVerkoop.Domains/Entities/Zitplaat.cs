using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Zitplaat
{
    public int ZitplaatsId { get; set; }

    public int SectionId { get; set; }

    public int Rij { get; set; }

    public string Stoel { get; set; } = null!;

    public virtual ICollection<Abonnement> Abonnements { get; set; } = new List<Abonnement>();

    public virtual Section Section { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
