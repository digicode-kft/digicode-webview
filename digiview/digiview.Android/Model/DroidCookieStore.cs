using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using digiview.Model;
using System.Linq;

[assembly: Xamarin.Forms.Dependency(typeof(digiview.Droid.Model.DroidCookieStore))]
namespace digiview.Droid.Model {
    public class DroidCookieStore : IPlatformCookieStore {

        public Int32 GetUserID() {
            CookieCollection cc = DroidCookieStore.GetCookieCollection();
            return DroidCookieStore.GetUserIDFromList(cc);
        }

        public static Int32 GetUserIDFromList(CookieCollection cc) {
            Int32 user_id = 0;

            foreach (Cookie c in cc) {
                try {
                    if (c.Name.Equals("user_id")) {
                        user_id = Convert.ToInt32(c.Value);
                    }
                } catch { }
            }

            return user_id;
        }

        public static Int32 GetUserIDFromCall(CookieCollection c) {
            /*
            Uri baseUrl = new Uri(digiview.Helper.SettingsHelper.GetPath());
            CookieContainer container = new CookieContainer();
            container.Add(baseUrl, c);
            HttpClient tempClient = new HttpClient(new NativeMessageHandler() { CookieContainer = container });
            tempClient.BaseAddress = baseUrl;
            HttpResponseMessage response = await tempClient.GetAsync("");
            string responseString = await response.Content.ReadAsStringAsync();
            responseString = responseString.Split(new string[] { "\"data\":" }, StringSplitOptions.None)[1];
            responseString = responseString.Substring(0, responseString.Length - 2);
            return responseString;
            */

            return 0;
        }

        public static CookieCollection GetCookieCollection() {
            var cookieHeader = CookieManager.Instance.GetCookie(digiview.Helper.SettingsHelper.GetPath());

            Android.Webkit.CookieManager cookieManager = Android.Webkit.CookieManager.Instance;
            cookieManager.SetAcceptCookie(true);
            string[] temp = cookieManager.GetCookie(digiview.Helper.SettingsHelper.GetPath()).Split(';');

            CookieCollection cookies = new CookieCollection();

            for (int i = 0; i < temp.Length; i++) {
                temp[i] = temp[i].Trim();
                string[] val = temp[i].Split('=');
                Cookie c = new Cookie();
                c.Name = val[0];
                c.Value = val[1];
                for (int j = 2; j < val.Length; j++) {
                    c.Value += "=" + val[j];
                }
                c.Path = "/";
                cookies.Add(c);
            }

            return cookies;
        }
    }
}