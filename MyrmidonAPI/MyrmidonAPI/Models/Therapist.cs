using System;
using System.Collections.Generic;

namespace MyrmidonAPI.Models;

public partial class Therapist
{
    public Guid TherapistId { get; set; }

    public Guid Id { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Patient> Patients { get; } = new List<Patient>();
}
