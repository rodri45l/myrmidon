using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MyrmidonAPI.Models;

public class User : IdentityUser<Guid>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid Id
    {
        get => base.Id;
        set => base.Id = value;
    }
    // Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string PostalCode { get; set; } = null!;

    public string Address { get; set; } = null!;

    public bool Sex { get; set; }

    public string? Gender { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<JournalEntry> JournalEntries { get; } = new List<JournalEntry>();

    public virtual ICollection<Mood> Moods { get; } = new List<Mood>();

    public virtual ICollection<Patient> Patients { get; } = new List<Patient>();

    public virtual ICollection<SessionToken> SessionTokens { get; } = new List<SessionToken>();

    public virtual ICollection<Tension> Tensions { get; } = new List<Tension>();

    public virtual ICollection<Therapist> Therapists { get; } = new List<Therapist>();

    public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();
}