﻿using System;
using System.Collections.Generic;

namespace azuretest.Models;

public partial class UserInformation
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Salt { get; set; }

    public int? Age { get; set; }

    public int? Weight { get; set; }

    public int? Height { get; set; }

    public string? Gender { get; set; }

    public int? Goal { get; set; }

    public int? ActivityLevel { get; set; }

    public virtual ICollection<MealsHistory> MealsHistories { get; } = new List<MealsHistory>();
}
