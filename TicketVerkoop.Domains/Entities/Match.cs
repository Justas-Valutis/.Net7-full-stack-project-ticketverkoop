using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Match
{
    public int MatchId { get; set; }

    public int StadiumId { get; set; }

    public int PloegThuisId { get; set; }

    public int PloegUitId { get; set; }

    public DateTime Datum { get; set; }

    public virtual Ploeg PloegThuis { get; set; } = null!;

    public virtual Ploeg PloegUit { get; set; } = null!;

    public virtual Stadium Stadium { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
