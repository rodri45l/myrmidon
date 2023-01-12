namespace MyrmidonAPI.Models;

public class Patient
{
    public Guid PatientId { get; set; }

    public string? MedicalHistory { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Therapist> Therapists { get; } = new List<Therapist>();
}