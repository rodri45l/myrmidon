namespace MyrmidonAPI.Dtos.Appointment;

public class AddAppointmentDto
{
    public DateTime Date { get; set; }

    public string? Notes { get; set; }
    
    public virtual ICollection<Models.User> Users { get; } = new List<Models.User>();
}