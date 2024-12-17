using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class Notification
{
    public int Id { get; set; }

    public TimeOnly? Timestamp { get; set; }

    public string? Message { get; set; }

    public string? UrgencyLevel { get; set; }

    public string? ReadStatus { get; set; }

    public virtual ICollection<Learner> Learners { get; set; } = new List<Learner>();
}
