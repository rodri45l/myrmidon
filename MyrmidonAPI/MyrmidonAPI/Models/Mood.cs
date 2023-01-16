using System;
using System.Collections.Generic;

namespace MyrmidonAPI.Models;

public partial class Mood
{
    public int MoodId { get; set; }

    public int Rating { get; set; }

    public DateTime Date { get; set; }

    public Guid Id { get; set; }

    public virtual User User { get; set; } = null!;
}
