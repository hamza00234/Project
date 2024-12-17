using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class SkillProgression
{
    public int Id { get; set; }

    public int? ProficiencyLevel { get; set; }

    public int? LearnerId { get; set; }

    public string? SkillName { get; set; }

    public TimeOnly? Timestamp { get; set; }

    public virtual Skill? Skill { get; set; }
}
