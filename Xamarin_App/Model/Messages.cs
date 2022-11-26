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
    public class StartServiceMessage { }
    public class StopServiceMessage { }
    public class StepCountMessage
    {
        public int StepCount { get; set; }

        public double CaloriesBurnt {get; set; }
        public double Distance { get; set; }
    }

}