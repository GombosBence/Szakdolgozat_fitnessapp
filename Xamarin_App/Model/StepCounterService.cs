using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Locations;
using Android.Net.Wifi.Aware;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using Java.Interop;
using Java.Lang;
using Java.Util;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Szakdolgozat.API;
using Szakdolgozat.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Math = Java.Lang.Math;
using Task = System.Threading.Tasks.Task;
using Thread = System.Threading.Thread;

[assembly: Xamarin.Forms.Dependency(typeof(StepCounterService))]
namespace Szakdolgozat.Model
{
    [Service]
    public class StepCounterService : Service, IStepCounter, ISensorEventListener
    {
        ConnectionString connectionString = new ConnectionString();

        int id;
        Interface1 api;
        CancellationTokenSource _cts;
        private int stepCount = 0;
        public const int SERVICE_RUNNING_NOTIFICATION_ID = 10000;

       
        public override IBinder OnBind(Intent intent)
        {
            
            return null;
        }

        public async void GetStepsAsync()
        {
            stepCount =  await api.getSteps(1, DateTime.Now);
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (intent.HasExtra("userId"))
            {
                id = intent.GetIntExtra("userId", 0);
            }
            api = RestService.For<Interface1>(connectionString.getConnection());
            _cts = new CancellationTokenSource();
            SensorManager sManager = Android.App.Application.Context.GetSystemService(Context.SensorService) as SensorManager;
            GetStepsAsync();
            

            Sensor stepDetectorSensor = sManager.GetDefaultSensor(SensorType.StepDetector);
            sManager.RegisterListener(this, stepDetectorSensor, SensorDelay.Normal);

            Notification notification = new NotificationHelper().GetServiceStartedNotification();
            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);

            Task.Run(async () =>
            {
                while (true)
                {

                    try
                    {
                        
                        int i = await api.saveSteps(1, stepCount, DateTime.Now); 

                        var message = new StepCountMessage
                        {
                            StepCount = stepCount
                        };
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MessagingCenter.Send<StepCounterService, int>(this, "StepCount", message.StepCount);
                            
                        });
                        
                    }
                    catch (Android.OS.OperationCanceledException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            });

            return StartCommandResult.Sticky;
        }

        public void StopSensorService()
        {
            Intent intent = new Intent(Android.App.Application.Context, typeof(StepCounterService));

            Android.App.Application.Context.StopService(intent);
        }
        public void InitSensorService(int userId)
        {
            Intent intent = new Intent(Android.App.Application.Context, typeof(StepCounterService));
            intent.PutExtra("userId", userId);

            Android.App.Application.Context.StartForegroundService(intent);

        }


        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            Console.WriteLine("OnAccuracyChangedCalled");
        }

        public async void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type == SensorType.StepDetector)
            {
                stepCount++;
               
            }
        }
    }
}