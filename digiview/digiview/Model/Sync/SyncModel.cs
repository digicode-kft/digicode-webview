using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Xam.Plugin.WebView.Abstractions;

namespace digiview.Models.Sync {
    public class SyncModel {
        private static SyncModel instance = null;

        private SyncModel() {
        }

        public static SyncModel GetInstance() {
            if (SyncModel.instance == null) {
                SyncModel.instance = new SyncModel();
            }

            return SyncModel.instance;
        }

        CancellationTokenSource tokenSource = null;

        private Boolean is_running = false;
        public Boolean IsRunning() {
            return this.is_running;
        }

        public async void Start() {
            try {
                if (this.tokenSource != null) {
                    this.tokenSource.Cancel();
                }

                this.tokenSource = new CancellationTokenSource();
                this.is_running = true;

                Boolean b = await DoSync();
            } catch(Exception ex) {
                Console.WriteLine("Exception 0: " + ex.Message);
            }
        }

        public void Stop() {
            if (this.tokenSource != null) {
                this.tokenSource.Cancel();
            }

            this.is_running = false;
        }

        public async void Trigger() {
            if (this.IsRunning()) {
                this.Stop();
            } else {
                this.Start();
            }
        }

        public async void Restart() {
            this.Stop();
            this.Start();
        }
        
        public Task<Boolean> DoSync() {
            return Task<Boolean>.Run(() => {
                try {
                    while (true) {

                        try {
                            var cookie = Xamarin.Forms.DependencyService.Get<Model.IPlatformCookieStore>();
                            Int32 user_id = cookie.GetUserID();
                            
                            if (user_id > 0) {
                                PushApiModel cam = new PushApiModel();
                                List<Push> list = cam.Get(user_id);

                                foreach (Push p in list) {
                                    digiview.Helper.NotificationHelper.ShowNotification(p.id, p.title, p.description);
                                    cam.Sended(p.id);
                                }
                            }
                        } catch (Exception ex) {
                            Console.WriteLine("SyncModel Domain Exception from  {0}", ex.ToString());
                        }

                        Thread.Sleep(5 * 1000);
                    }
                } catch (OperationCanceledException) {
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }

                return true;
            }, this.tokenSource.Token);
        }
    }
}
