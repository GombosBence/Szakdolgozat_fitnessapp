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
using Refit;

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
        Interface1 api;
        int loggedInUserId;
        List<DateTime> lastSevenDays = new List<DateTime>();
        int[] lastSevenDaysSteps = new int[6];
        ConnectionString connectionString = new ConnectionString();

       

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = LayoutInflater.Inflate(Resource.Layout.StepCounter, container, false);
            stepsTv = view.FindViewById<TextView>(Resource.Id.stepsTv);
            stepGoalSpinner = view.FindViewById<Spinner>(Resource.Id.stepGoalSpinner);
            stepProgressBar = view.FindViewById<ProgressBar>(Resource.Id.stepProgressBar);
            distanceTv = view.FindViewById<TextView>(Resource.Id.distanceTv);
            caloriestBurntTv = view.FindViewById<TextView>(Resource.Id.caloriesBurntTv);
            chartView = view.FindViewById<ChartView>(Resource.Id.chartView1);
            api = RestService.For<Interface1>(connectionString.getConnection());
            loggedInUserId = Arguments.GetInt("uid");
            getLastSevenDaysSteps();
           




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

        public async void getStepGoal()
        {
            var result = api.getStepGoal(loggedInUserId);
            switch (result.Result)
            {
                case 1000:
                    stepGoalSpinner.SetSelection(0);
                    break;
                case 2000:
                    stepGoalSpinner.SetSelection(1);
                    break;
                case 5000:
                    stepGoalSpinner.SetSelection(2);
                    break;
                case 10000:
                    stepGoalSpinner.SetSelection(3);
                    break;
                case 15000:
                    stepGoalSpinner.SetSelection(4);
                    break;
                case 20000:
                    stepGoalSpinner.SetSelection(5);
                    break;
            }
            

        }

        public async void getLastSevenDaysSteps()
        {
        
            for (int i = 1; i <= 7; i++)
            {
                lastSevenDays.Add(DateTime.Now.AddDays(-1 * i));
            }

            lastSevenDaysSteps = await api.getLastWeekSteps(loggedInUserId, lastSevenDays);
            drawChart();

        }

        private void drawChart()
        {
            
            List<ChartEntry> DataList = new List<ChartEntry>();
            for (int i = lastSevenDaysSteps.Length -1; i >= 0; i--)
            {
                DataList.Add(new ChartEntry(lastSevenDaysSteps[i])
                {
                    Label = lastSevenDays.ElementAt(i).ToString("MM-dd"),
                    ValueLabel = lastSevenDaysSteps[i].ToString(),
                    Color = SKColor.Parse("#B7D2CE")
                });
            }

            var chart = new BarChart() { Entries = DataList, LabelTextSize = 30f};
            chartView.Chart = chart;
        }


        private async void spinner_Itemselceted(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = sender as Spinner;
            getStepGoal();
            stepProgressBar.Max = Int32.Parse(spinner.SelectedItem.ToString());
            


            var result = await api.setGoal(loggedInUserId, Int32.Parse(spinner.SelectedItem.ToString()), DateTime.Now);
        }
     
    }
}