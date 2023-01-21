namespace MyrmidonAPI.Dtos.Mood;

public class AddMoodDto
{
    public int Rating { get; set; }

    public DateTime Date { get; set; }

    public Guid UserId { get; set; }
}