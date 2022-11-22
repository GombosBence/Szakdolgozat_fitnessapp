using System;
using System.Collections.Generic;

namespace azuretest.Models;

public partial class StepsHistory
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? Steps { get; set; }

    public DateTime Date { get; set; }

    public double? CaloriesBurnt { get; set; }

    public double? Distance { get; set; }

    public TimeSpan? TimeSpent { get; set; }

    public virtual UserInformation User { get; set; } = null!;
}
