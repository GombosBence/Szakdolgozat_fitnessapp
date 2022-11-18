using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Szakdolgozat.Model
{
    internal class NotificationHelper
    {
        private static string foregroundChannelId = "9003";
        private static Context context = global::Android.App.Application.Context;

        public Notification GetServiceStartedNotification()
        {
            var intent = new Intent(context, typeof(Main_Page));
            intent.AddFlags(ActivityFlags.SingleTop);
            intent.PutExtra("Title", "Message");

            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);
            var notificationBuilder = new NotificationCompat.Builder(context, foregroundChannelId)
                          .SetContentTitle("Stepcount tracker")
                          .SetContentText("Your steps are being tracked")
                          .SetSmallIcon(Resource.Drawable.abc_btn_default_mtrl_shape)
                          .SetOngoing(true)
                          .SetContentIntent(pendingIntent);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationChannel notificationChannel = new
                    NotificationChannel(foregroundChannelId, "Title", NotificationImportance.High);
                notificationChannel.Importance = NotificationImportance.High;
                notificationChannel.EnableLights(true);
                notificationChannel.EnableVibration(true);
                notificationChannel.SetShowBadge(true);

                var notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
                if (notificationManager != null)
                {
                    notificationBuilder.SetChannelId(foregroundChannelId);
                    notificationManager.CreateNotificationChannel(notificationChannel);
                }
            }
           

            return notificationBuilder.Build();
        }


    }
}