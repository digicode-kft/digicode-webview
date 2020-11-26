using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ObjCRuntime;
using digiview.iOS.Model;
using UIKit;
using UserNotifications;

namespace digiview.iOS {
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {

        protected string deviceToken = string.Empty;
        public string DeviceToken {
            get {
                return deviceToken;
            }
        }

        // You have 17 seconds to return from this method, or iOS will terminate your application.
        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            try {
                // dependency
                Xamarin.Forms.DependencyService.Register<Model.IOSCookieStore>();
                Xamarin.Forms.DependencyService.Register<Model.Notification>();

                global::Xamarin.Forms.Forms.Init();
                LoadApplication(new App());

                Xam.Plugin.WebView.iOS.FormsWebViewRenderer.Initialize();
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                // Notification settings
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0)) {
                    UIUserNotificationType userNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                    UIUserNotificationSettings notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(userNotificationTypes, null);
                    UIApplication.SharedApplication.RegisterUserNotificationSettings(notificationSettings);
                    UIApplication.SharedApplication.RegisterForRemoteNotifications();
                } else {
                    UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                    UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
                }
                
                // Check for notifications
                if (options != null) {
                    // Check for local notification
                    if (options.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey)) {
                        var localNotification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
                        if (localNotification != null) {
                            UIAlertController okayAlertController = UIAlertController.Create(localNotification.AlertAction, localNotification.AlertBody, UIAlertControllerStyle.Alert);
                            okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                            Window.RootViewController.PresentViewController(okayAlertController, true, null);

                            // reset our badge
                            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
                        }
                    }

                    // Check for remote notification
                    if (options.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey)) {
                        NSDictionary remoteNotification = (NSDictionary) options[UIApplication.LaunchOptionsRemoteNotificationKey];
                        if (remoteNotification != null) {
                            //NotificationHelper.processNotification(userInfo, true, false);
                        }
                    }
                }

                return base.FinishedLaunching(app, options);
            } catch(Exception ex) {
                Console.WriteLine("Domain Exception from  {0}", ex.ToString());
            }

            return true;
        }

        private void CurrentDomain_UnhandledException(Object s, UnhandledExceptionEventArgs e) {
            var ex = e.ExceptionObject as Exception;
            Console.WriteLine("Domain Exception from  {0}: {1}", s.GetType(), ex != null ? ex.ToString() : e.ToString());
        }

        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification) {
            UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
            okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(okayAlertController, true, null);

            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        }
        /*
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler) {
            //ProcessNotification(userInfo, false);
            completionHandler(UIBackgroundFetchResult.NewData);
            // base.DidReceiveRemoteNotification(application, userInfo, completionHandler);
        }
        */
        //when you app in foreground
        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo) {
            /*
            if (userInfo != null) {
                var message = userInfo.ValueForKey(new NSString("yourTextKey")) as NSString;
            }
            */
        }

        // K3XRZ55J65
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken) {
            deviceToken = deviceToken.ToString();
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error) {
            new UIAlertView("Error registering push notifications", error.LocalizedDescription, null, "OK", null).Show();
        }

    }
}
