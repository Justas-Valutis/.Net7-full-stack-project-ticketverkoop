using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Ploeg
{
    public int PloegId { get; set; }

    public string Naam { get; set; } = null!;

    public int ThuisStadiumId { get; set; }

    public virtual ICollection<Abonnement> Abonnements { get; set; } = new List<Abonnement>();

    public virtual ICollection<Match> MatchPloegThuis { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchPloegUits { get; set; } = new List<Match>();

    public virtual Stadium ThuisStadium { get; set; } = null!;
}
