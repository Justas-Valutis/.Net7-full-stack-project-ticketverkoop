using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class Ring
{
    public int RingId { get; set; }

    public int StadiumId { get; set; }

    public string ZoneLocatie { get; set; } = null!;

    public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

    public virtual Stadium Stadium { get; set; } = null!;
}
