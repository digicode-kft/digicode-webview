using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using digiview.Model;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(digiview.Droid.Model.Notification))]
namespace digiview.Droid.Model {

    [Service(Exported = false), IntentFilter(new[] { "com.google.android.c2dm.intent.RECEIVE" })]
    public class Notification : INotification {
        private String channel_id = "DigiCodeChanelID88";

        public void ShowNotification(Int32 id, String title, String msg) {
            try {
                var activity = (Droid.MainActivity) Forms.Context;

                // Set up an intent so that tapping the notifications returns to this app:
                Intent intent = new Intent(activity, typeof(MainActivity));

                // Create a PendingIntent; we're only using one PendingIntent (ID = 0):
                const int pendingIntentId = 0;
                PendingIntent pendingIntent = PendingIntent.GetActivity(activity, pendingIntentId, intent, PendingIntentFlags.OneShot);

                // Instantiate the builder and set notification elements, including pending intent:
                NotificationCompat.Builder builder = new NotificationCompat.Builder(activity, this.channel_id)
                    .SetContentIntent(pendingIntent)
                    .SetContentTitle(title)
                    .SetContentText(msg)
                    .SetSmallIcon(Resource.Drawable.ic_stat_info)
                    .SetLargeIcon(Android.Graphics.BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.ic_stat_info))
                    .SetSound(Android.Media.RingtoneManager.GetDefaultUri(Android.Media.RingtoneType.Notification))
                    .SetVibrate(new long[] { 1000, 1000 });

                // Build the notification:
                Android.App.Notification notification = builder.Build();

                // Get the notification manager:
                NotificationManager notificationManager =
                    activity.GetSystemService(Context.NotificationService) as NotificationManager;

                // Publish the notification:
                const int notificationId = 0;
                notificationManager.Notify(notificationId, notification);

            } catch (Exception ex) {
                Console.WriteLine("Notify: " + ex.Message.ToString());
            }
        }

        public void CreateNotificationChannel() {
            var activity = (Droid.MainActivity) Forms.Context;

            if (Build.VERSION.SdkInt < BuildVersionCodes.O) {
                return;
            }

            var channelName = "DigiCode Push"; // Resources.GetString(Resource.String.channel_name);
            var channelDescription = "DigiCode Push Chanel"; // GetString(Resource.String.channel_description);
            var channel = new NotificationChannel(this.channel_id, channelName, NotificationImportance.Default) {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager) activity.GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}