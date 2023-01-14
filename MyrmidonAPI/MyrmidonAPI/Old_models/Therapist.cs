namespace MyrmidonAPI.Models;

public class Therapist
{
    public Guid TherapistId { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; } = new List<Patient>();
}