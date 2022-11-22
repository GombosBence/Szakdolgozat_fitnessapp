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

namespace Szakdolgozat.API
{
    public interface IStepCounter
    {
        //int Steps { get; set; }
       // bool IsAvaiable();
        void InitSensorService(int userId);
        void StopSensorService();

    }
}