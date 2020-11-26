using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace digiview.Models.Sync {
    public class BaseApiModel<T> {
        protected CookieContainer cookieContainer = new CookieContainer();
        protected CookieCollection cookieCollection = new CookieCollection();
        protected HttpWebResponse lasthttpResponse = null;

        public String code {
            get;
            set;
        }

        public T message {
            get;
            set;
        }

        public String status {
            get;
            set;
        }

        public String get_api_host(String method) {
            return this.api_host + method;
        }

        public String get_api_key() {
            return this.api_key;
        }
        public String get_api_username() {
            return this.api_username;
        }
        public String get_api_password() {
            return this.api_password;
        }

        protected HttpClient client;
        private String api_host;
        private String api_key;
        private String api_username;
        private String api_password;

        public BaseApiModel() {
            this.client = new HttpClient();
            this.client.MaxResponseContentBufferSize = 256000;

            try {
                //Setting.SettingApiModel sam = new Setting.SettingApiModel(1);
                this.api_host = "http://www.digiview.eu/api/";
                this.api_key = "";
                this.api_username = "";
                this.api_password = "";
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
