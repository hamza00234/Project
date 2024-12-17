﻿using System;
using System.Collections.Generic;

namespace WebApplication4.Models;

public partial class ModuleContent
{
    public int ModuleId { get; set; }

    public int CourseId { get; set; }

    public string ContentType { get; set; } = null!;

    public virtual Module Module { get; set; } = null!;
}