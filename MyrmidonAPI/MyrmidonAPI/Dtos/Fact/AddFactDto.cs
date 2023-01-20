namespace MyrmidonAPI.Dtos.Fact;

public class AddFactDto
{
    public string Fact1 { get; set; } = null!;

    public DateTime? LastShown { get; set; }
}