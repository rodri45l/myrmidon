namespace MyrmidonAPI.Models;

public class User
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string PostalCode { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool Sex { get; set; } // True for female, False for male.

    public string? Gender { get; set; }

    public string? Password { get; set; }


    public string UserType { get; set; } = null!;

    public virtual ICollection<JournalEntry> JournalEntries { get; } = new List<JournalEntry>();

    public virtual ICollection<Mood> Moods { get; } = new List<Mood>();

    public virtual ICollection<Patient> Patients { get; } = new List<Patient>();

    public virtual SessionToken? SessionToken { get; set; }

    public virtual ICollection<Tension> Tensions { get; } = new List<Tension>();

    public virtual ICollection<Therapist> Therapists { get; } = new List<Therapist>();

    public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();
}