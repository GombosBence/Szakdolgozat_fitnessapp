using System;
using System.Collections.Generic;

namespace azuretest.Models;

public partial class Milestone
{
    public int Id { get; set; }

    public string MilestoneName { get; set; } = null!;

    public int Goal { get; set; }

    public int Score { get; set; }

    public virtual ICollection<UserMilestone> UserMilestones { get; } = new List<UserMilestone>();
}
