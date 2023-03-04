using System;
using System.Collections.Generic;

namespace azuretest.Models;

public partial class UserMilestone
{
    public int Id { get; set; }

    public int MilestoneId { get; set; }

    public int UserId { get; set; }

    public virtual Milestone Milestone { get; set; } = null!;

    public virtual UserInformation User { get; set; } = null!;
}
