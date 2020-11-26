using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using digiview.Model;
using UserNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(digiview.iOS.Model.Notification))]
namespace digiview.iOS.Model {
    public class Notification : INotification {
        public void ShowNotification(Int32 id, String title, String msg) {
            // create the notification
            var notification = new UILocalNotification();

            // set the fire date (the date time in which it will fire)
            notification.FireDate = NSDate.FromTimeIntervalSinceNow(5);

            // configure the alert
            notification.AlertTitle = title;
            notification.AlertAction = "Értesítés";
            notification.AlertBody = msg;

            // modify the badge
            notification.ApplicationIconBadgeNumber = 1;

            // set the sound to be the default sound
            notification.SoundName = UILocalNotification.DefaultSoundName;

            // schedule it
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);

            // remote
            var content = new UNMutableNotificationContent();
            content.Title = title;
            content.Subtitle = title;
            content.Body = msg;
            content.Badge = 1;

            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);

            var requestID = "sampleRequest";
            var request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

            UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                if (err != null) {
                    // Do something with error...
                    new UIAlertView("Error push notifications", "xx", null, "OK", null).Show();
                }
            });
        }
    }
}