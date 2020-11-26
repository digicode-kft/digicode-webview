using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android.Content;
using Android.Support.V4.App;

namespace digiview.Droid {
    [Activity(Label = "digiview", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, NoHistory = true)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
        protected override void OnCreate(Bundle bundle) {
            try {
                // dependency
                Xamarin.Forms.DependencyService.Register<Model.DroidCookieStore>();
                Xamarin.Forms.DependencyService.Register<Model.Notification>();
                
                // view
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                AndroidEnvironment.UnhandledExceptionRaiser += OnAndroidEnvironmentUnhandledExceptionRaiser;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
                    Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                    //Window.SetStatusBarColor(Android.Graphics.Color.DarkBlue);
                }

                base.OnCreate(bundle);

                global::Xamarin.Forms.Forms.Init(this, bundle);
                LoadApplication(new App());


                // create channel
                Model.Notification n = new Model.Notification();
                n.CreateNotificationChannel();

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private void CurrentDomain_UnhandledException(Object s, UnhandledExceptionEventArgs e) {
            var ex = e.ExceptionObject as Exception;
            Console.WriteLine("Domain Exception from  {0}: {1}", s.GetType(), ex != null ? ex.ToString() : e.ToString());
        }

        private static void TaskSchedulerOnUnobservedTaskException(object s, UnobservedTaskExceptionEventArgs e) {
            var ex = e.Exception as Exception;
            Console.WriteLine("Domain Exception from  {0}: {1}", s.GetType(), ex != null ? ex.ToString() : e.ToString());
        }

        private void OnAndroidEnvironmentUnhandledExceptionRaiser(object s, RaiseThrowableEventArgs e) {
            var ex = e.Exception as Exception;
            Console.WriteLine("Domain Exception from  {0}: {1}", s.GetType(), ex != null ? ex.ToString() : e.ToString());

            e.Handled = true;
        }
    }
}

