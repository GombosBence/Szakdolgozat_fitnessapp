using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szakdolgozat.Model
{
    public class Milestone
    {

        public Milestone(string milestoneName, int goal, int score, int prog)
        {
            MilestoneName = milestoneName;
            Goal = goal;
            Score = score;
            progress = prog;
        }

        public string MilestoneName { get; set; } = null!;

        public int Goal { get; set; }

        public int Score { get; set; }

        public int progress { get; set; }
    }
}