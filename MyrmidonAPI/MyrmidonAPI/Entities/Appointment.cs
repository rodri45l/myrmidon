using System;
using System.Collections.Generic;

namespace MyrmidonAPI.Entities;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public DateTime Date { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
