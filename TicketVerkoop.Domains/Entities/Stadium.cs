using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Stadium
{
    public int StadiumId { get; set; }

    public string Naam { get; set; } = null!;

    public string Stad { get; set; } = null!;

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();

    public virtual ICollection<Ploeg> Ploegs { get; set; } = new List<Ploeg>();

    public virtual ICollection<Ring> Rings { get; set; } = new List<Ring>();
}
