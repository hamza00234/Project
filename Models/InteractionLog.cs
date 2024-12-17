using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class InteractionLog
{
    public int LogId { get; set; }

    public int? ActivityId { get; set; }

    public int? LearnerId { get; set; }

    public TimeOnly? Duration { get; set; }

    public TimeOnly? Timestamp { get; set; }

    public string? ActionType { get; set; }

    public virtual LearningActivity? Activity { get; set; }

    public virtual Learner? Learner { get; set; }
}
