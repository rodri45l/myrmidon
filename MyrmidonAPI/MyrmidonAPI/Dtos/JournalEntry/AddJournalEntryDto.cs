namespace MyrmidonAPI.Dtos.JournalEntry;

public class AddJournalEntryDto
{
    public string JournalEntry1 { get; set; } = null!;

    public DateTime Date { get; set; }

    public Guid Id { get; set; }
}