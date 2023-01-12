using System;
using System.Collections.Generic;

namespace MyrmidonAPI.Entities;

public partial class SessionToken
{
    public Guid TokenId { get; set; }

    public Guid UserId { get; set; }

    public DateTime ExpirationTime { get; set; }

    public string IpAddress { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
