namespace MyrmidonAPI.Models;

public class Tension
{
    public int TensionId { get; set; }

    public int Rating { get; set; }

    public DateTime Date { get; set; }

    public Guid Id { get; set; }

    public virtual User User { get; set; } = null!;
}