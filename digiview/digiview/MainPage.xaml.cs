using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using System.Net.Http;
using Xam.Plugin.WebView;
using Xam.Plugin.WebView.Abstractions;
using System.Threading;

namespace digiview {
    public partial class MainPage : ContentPage {

        public MainPage() {
            try {
                InitializeComponent();

                FormsWebView fwv = new FormsWebView();
                fwv.Source = digiview.Helper.SettingsHelper.GetPath();
                fwv.IsVisible = true;
                fwv.MinimumWidthRequest = 200;
                fwv.MinimumHeightRequest = 200;                
                fwv.HorizontalOptions = LayoutOptions.FillAndExpand;
                fwv.VerticalOptions = LayoutOptions.FillAndExpand;
                fwv.EnableGlobalHeaders = true;
                fwv.OnContentLoaded += this.Fwv_OnContentLoaded;

                ((digiview.App) App.Current).SetWebView(fwv);   
                this.web_grid.Children.Add(fwv);

                Models.Sync.SyncModel.GetInstance().Start();
            } catch(TimeoutException te) {
                Console.WriteLine(te.Message);
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        private void Fwv_OnContentLoaded(Object sender, EventArgs e) {
            //Models.Sync.SyncModel.GetInstance().Start();
        }

        protected override Boolean OnBackButtonPressed() {
            return true;
        }
    }
}
