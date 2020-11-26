using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Foundation;
using digiview.Model;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(digiview.iOS.Model.IOSCookieStore))]
namespace digiview.iOS.Model {
    public class IOSCookieStore : IPlatformCookieStore {

        public Int32 GetUserID() {
            Int32 user_id = 0;

            try {
                NSHttpCookieStorage storage = NSHttpCookieStorage.SharedStorage;
                user_id = Convert.ToInt32(storage.Cookies.ToList().Where(w => w.Name.Equals("user_id")).FirstOrDefault().Value.ToString());
                /*
                UIAlertView _error = new UIAlertView("My Title Text", "user_id: " + user_id , null, "Ok", null);
                _error.Show();
                */
            } catch {
            }

            return user_id;
        }
    }
}