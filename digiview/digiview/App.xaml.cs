using System;
using System.Threading.Tasks;
using Xam.Plugin.WebView.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace digiview {
    public partial class App : Application {
        public FormsWebView web_content = null;

        public App() {
            try {
                InitializeComponent();

                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

                MainPage = new MainPage();
            } catch(Exception ex) {
                Console.WriteLine("Domain Exception from  {0}", ex.ToString());
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

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
        
        public FormsWebView GetWebView() {
            return this.web_content;
        }
        public void SetWebView(FormsWebView webview) {
            this.web_content = webview;
        }

    }
}
