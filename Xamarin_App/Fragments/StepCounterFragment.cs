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
using DocumentFormat.OpenXml.Spreadsheet;
using Microcharts.Droid;
using Microcharts;
using SkiaSharp;

namespace Szakdolgozat.Fragments
{

    

    [Obsolete]
    public class StepCounterFragment : Android.Support.V4.App.Fragment //, ISensorEventListener
    {

        private int StepsCounter = 0;
        TextView stepsTv;
        Spinner stepGoalSpinner;
        ProgressBar stepProgressBar;
        TextView distanceTv;
        TextView caloriestBurntTv;
        ChartView chartView;

       

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = LayoutInflater.Inflate(Resource.Layout.StepCounter, container, false);
            stepsTv = view.FindViewById<TextView>(Resource.Id.stepsTv);
            stepGoalSpinner = view.FindViewById<Spinner>(Resource.Id.stepGoalSpinner);
            stepProgressBar = view.FindViewById<ProgressBar>(Resource.Id.stepProgressBar);
            distanceTv = view.FindViewById<TextView>(Resource.Id.distanceTv);
            caloriestBurntTv = view.FindViewById<TextView>(Resource.Id.caloriesBurntTv);
            chartView = view.FindViewById<ChartView>(Resource.Id.chartView1);
            drawChart();




            MessagingCenter.Subscribe<StepCounterService, int>(this, "StepCount",  (sender, arg) => 
            {
                StepsCounter = arg;
                stepProgressBar.Progress = StepsCounter;
                stepsTv.Text = StepsCounter.ToString();
                caloriestBurntTv.Text = (StepsCounter * 0.04).ToString() + " Kcal";
                distanceTv.Text = (StepsCounter / 1200.0).ToString("0.00") + " Km";
                
            });

            
            

            stepGoalSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_Itemselceted);
            var adapter = ArrayAdapter.CreateFromResource
                (Activity, Resource.Array.stepsArray, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            stepGoalSpinner.Adapter = adapter;
            stepProgressBar.Max = Int32.Parse(stepGoalSpinner.SelectedItem.ToString());


            return view;
        }

        private void drawChart()
        {
            List<ChartEntry> DataList = new List<ChartEntry>();
            DataList.Add(new ChartEntry(1000)
            {
               Label = "Day1",
               ValueLabel = "1000",
               Color = SKColor.Parse("#266489")
            });
            DataList.Add(new ChartEntry(10000)
            {
                Label = "Day2",
                ValueLabel = "10000",
                Color = SKColor.Parse("#266489")
            });
            DataList.Add(new ChartEntry(2000)
            {
                Label = "Day3",
                ValueLabel = "2000",
                Color = SKColor.Parse("#266489")
            });
            DataList.Add(new ChartEntry(3000)
            {
                Label = "Day4",
                ValueLabel = "3000",
                Color = SKColor.Parse("#266489")
            });
            DataList.Add(new ChartEntry(4000)
            {
                Label = "Day5",
                ValueLabel = "4000",
                Color = SKColor.Parse("#266489")
            });
            DataList.Add(new ChartEntry(5000)
            {
                Label = "Day6",
                ValueLabel = "5000",
                Color = SKColor.Parse("#266489")
            });
            DataList.Add(new ChartEntry(10000)
            {
                Label = "Day7",
                ValueLabel = "10000",
                Color = SKColor.Parse("#266489")
            });

            var chart = new BarChart() { Entries = DataList, LabelTextSize = 30f, MaxValue = 20000 };
            chartView.Chart = chart;
        }


        private void spinner_Itemselceted(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            stepProgressBar.Max = Int32.Parse(spinner.SelectedItem.ToString());

        }
     
    }
}