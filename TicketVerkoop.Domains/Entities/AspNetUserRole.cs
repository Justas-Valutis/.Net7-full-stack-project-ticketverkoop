using System;
using System.Collections.Generic;

namespace TicketVerkoop.Domains.Entities;

public partial class AspNetUserRole
{
    public string UserId { get; set; } = null!;

    public string RoleId { get; set; } = null!;

    public virtual AspNetRole Role { get; set; } = null!;
}
