namespace MyrmidonAPI.Models;

public class Mood
{
    public int MoodId { get; set; }

    public int Rating { get; set; }

    public DateTime Date { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;
}