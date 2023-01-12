using System;
using System.Collections.Generic;

namespace MyrmidonAPI.Entities;

public partial class Fact
{
    public int FactId { get; set; }

    public string Fact1 { get; set; } = null!;

    public DateTime? LastShown { get; set; }
}
