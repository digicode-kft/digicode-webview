using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace digiview.Helper {
    public class NotificationHelper {
        public static void ShowNotification(Int32 id, String title, String msg) {
            var notification = Xamarin.Forms.DependencyService.Get<Model.INotification>();
            notification.ShowNotification(id, title, msg);
        }
    }
}
