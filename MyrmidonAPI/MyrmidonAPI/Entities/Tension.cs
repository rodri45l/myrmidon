using System;
using System.Collections.Generic;

namespace MyrmidonAPI.Entities;

public partial class Tension
{
    public int TensionId { get; set; }

    public int Rating { get; set; }

    public DateTime Date { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
