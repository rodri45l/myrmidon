using System;
using System.Collections.Generic;

namespace MyrmidonAPI.Models;

public partial class Patient
{
    public Guid PatientId { get; set; }

    public string? MedicalHistory { get; set; }

    public Guid Id { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Therapist> Therapists { get; } = new List<Therapist>();
}
