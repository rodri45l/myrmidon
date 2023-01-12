namespace MyrmidonAPI.Models;

public class JournalEntry
{
    public int JournalEntryId { get; set; }

    public string JournalEntry1 { get; set; } = null!;

    public DateTime Date { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;
}