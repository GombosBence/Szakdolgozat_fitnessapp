using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szakdolgozat.Model;
using View = Android.Views.View;
using Android.Hardware;
using ProgressBar = Android.Widget.ProgressBar;
using Android.Hardware.Display;
using Android.Support.V4.Content;
using Android;
using Android.Content.PM;
using Szakdolgozat.API;
using Java.Lang;
using Xamarin.Forms;
using Math = Java.Lang.Math;

namespace Szakdolgozat.Fragments
{

    

    [Obsolete]
    public class StepCounterFragment : Android.Support.V4.App.Fragment //, ISensorEventListener
    {

        private int StepsCounter = 0;
        TextView stepsTv;
        Spinner stepGoalSpinner;
        ProgressBar stepProgressBar;

       

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = LayoutInflater.Inflate(Resource.Layout.StepCounter, container, false);
            stepsTv = view.FindViewById<TextView>(Resource.Id.stepsTv);
            stepGoalSpinner = view.FindViewById<Spinner>(Resource.Id.stepGoalSpinner);
            stepProgressBar = view.FindViewById<ProgressBar>(Resource.Id.stepProgressBar);
            


            MessagingCenter.Subscribe<StepCounterService, int>(this, "StepCount",  (sender, arg) => 
            {
                StepsCounter = arg;
                stepProgressBar.Progress = StepsCounter;
                stepsTv.Text = StepsCounter.ToString();
            });

            
            

            stepGoalSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_Itemselceted);
            var adapter = ArrayAdapter.CreateFromResource
                (Activity, Resource.Array.stepsArray, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            stepGoalSpinner.Adapter = adapter;
            stepProgressBar.Max = Int32.Parse(stepGoalSpinner.SelectedItem.ToString());


            return view;
        }


        private void spinner_Itemselceted(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            stepProgressBar.Max = Int32.Parse(spinner.SelectedItem.ToString());

        }
     
    }
}