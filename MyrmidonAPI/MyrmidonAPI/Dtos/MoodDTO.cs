namespace MyrmidonAPI.Dtos;

public class MoodDTO
{
    public int MoodId { get; set; }

    public int Rating { get; set; }

    public DateTime Date { get; set; }

    public Guid UserId { get; set; }
}