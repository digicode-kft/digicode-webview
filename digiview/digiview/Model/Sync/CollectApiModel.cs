using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace digiview.Models.Sync {
    public class PushApiModel : BaseApiModel<Push> {

        public PushApiModel() : base() {

        }

        public List<Push> Get(Int32 user_id) {
            return this.Get(user_id.ToString());
        }

        public List<Push> Get(String user_id) {
            var uri = new Uri(this.get_api_host("push/get?user_id=" + user_id));
            List<Push> cm = new List<Push>();

            var httpWebRequest = (HttpWebRequest) WebRequest.Create(uri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.CookieContainer = this.cookieContainer;
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "GET";

            if (this.lasthttpResponse != null) {
                httpWebRequest.CookieContainer.Add(this.lasthttpResponse.Cookies);
            }

            httpWebRequest.Headers.Add("X-API-KEY", this.get_api_key());

            var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
            httpResponse.Cookies = cookieContainer.GetCookies(uri);
            this.lasthttpResponse = httpResponse;

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                try {
                    var result = streamReader.ReadToEnd();
                    var settings = new JsonSerializerSettings {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    if (!result.ToString().Equals("[]")) {
                        List<Push> jsondata = JsonConvert.DeserializeObject<List<Push>>(result.ToString(), settings);

                        if (jsondata.Count > 0) {
                            cm = jsondata;
                        } else {
                            throw new Exception(jsondata.ToString());
                        }
                    }
                } catch (Exception ex) {
                    Console.WriteLine("CollectApiModel exception 1: " + ex.Message);
                    throw ex;
                }
            }

            return cm;
        }

        public List<String> Sended(Int32 id) {
            var uri = new Uri(this.get_api_host("push/sended?id=" + id));
            List<String> cm = new List<String>();

            var httpWebRequest = (HttpWebRequest) WebRequest.Create(uri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.CookieContainer = this.cookieContainer;
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "GET";

            if (this.lasthttpResponse != null) {
                httpWebRequest.CookieContainer.Add(this.lasthttpResponse.Cookies);
            }

            httpWebRequest.Headers.Add("X-API-KEY", this.get_api_key());

            var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
            httpResponse.Cookies = cookieContainer.GetCookies(uri);
            this.lasthttpResponse = httpResponse;

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                try {
                    var result = streamReader.ReadToEnd();
                    var settings = new JsonSerializerSettings {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    List<String> jsondata = JsonConvert.DeserializeObject<List<String>>(result.ToString(), settings);

                    if (jsondata.Count > 0) {
                        cm = jsondata;
                    } else {
                        throw new Exception(jsondata.ToString());
                    }
                } catch (Exception ex) {
                    Console.WriteLine("CollectApiModel exception 2: " + ex.Message);
                    throw ex;
                }
            }

            return cm;
        }
    }
}
