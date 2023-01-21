namespace MyrmidonAPI.Dtos.Tension;

public class AddTensionDto
{
    public int Rating { get; set; }

    public DateTime Date { get; set; }

    public Guid UserId { get; set; }
}