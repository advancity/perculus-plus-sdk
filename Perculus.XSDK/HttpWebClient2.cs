using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace Perculus.XSDK
{
    class HttpWebClient
    {

        public static T GET<T>(string url, string method, string token, object query = null)
        {
            try
            {
                var client = new WebClient();
                var uri = ApiUri(url, $"{method}?{GetQueryString(query)}");
                client.Headers.Add("User-Agent", "PERCULUS_SDK");
                client.Headers.Add("Authorization", "Bearer " + token);
                var content = client.DownloadString(uri);
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                //WriteLog(ex.Message);
                T obj = default(T);
                return obj;
            }
        }

        public virtual HttpWebRequest CreateWebRequest(string uri, NameValueCollection collHeader, string RequestMethod, bool NwCred)
        {
            HttpWebRequest webrequest =
             (HttpWebRequest)WebRequest.Create(uri);
            webrequest.KeepAlive = false;
            webrequest.Method = RequestMethod;

            int iCount = collHeader.Count;
            string key;
            string keyvalue;

            for (int i = 0; i < iCount; i++)
            {
                key = collHeader.Keys[i];
                keyvalue = collHeader[i];
                webrequest.Headers.Add(key, keyvalue);
            }

            webrequest.ContentType = "text/html";
            //"application/x-www-form-urlencoded";

            if (ProxyServer.Length > 0)
            {
                webrequest.Proxy = new
                 WebProxy(ProxyServer, ProxyPort);
            }
            webrequest.AllowAutoRedirect = false;

            if (NwCred)
            {
                CredentialCache wrCache =
                        new CredentialCache();
                wrCache.Add(new Uri(uri), "Basic",
                  new NetworkCredential(UserName, UserPwd));
                webrequest.Credentials = wrCache;
            }
            //Remove collection elements
            collHeader.Clear();
            return webrequest;
        }

        public static T POST<T>(string url, string method, string token, object data)
        {
            try
            {
                HttpWebRequest webrequest =
                    CreateWebRequest(ReUri, collHeader,
                    RequestMethod, NwCred);
                var client = new WebClient();
                client.Headers.Add("User-Agent", "PERCULUS_SDK");
                client.Headers.Add("Authorization", "Bearer " + token);
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var content = client.UploadString(ApiUri(url, method), "POST", JsonConvert.SerializeObject(data));
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                //WriteLog(ex.Message);
                T obj = default(T);
                return obj;
            }
        }
        


        public static T POST_FORMDATA<T>(string Url, object data)
        {
            try
            {
                var client = new WebClient();
                client.Headers.Add("User-Agent", "PERCULUS_SDK");
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                var content = client.UploadString(Url, "POST", GetQueryString(data));
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                //WriteLog(ex.Message);
                T obj = default(T);
                return obj;
            }
        }


        public static T PUT<T>(string url, string method, string token, object data)
        {
            try
            {
                var client = new WebClient();
                client.Headers.Add("User-Agent", "PERCULUS_SDK");
                client.Headers.Add("Authorization", "Bearer " + token);
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var content = client.UploadString(ApiUri(url, method), "PUT", JsonConvert.SerializeObject(data));
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                //WriteLog(ex.Message);
                T obj = default(T);
                return obj;
            }
        }

        public static T DELETE<T>(string url, string method, string token)
        {
            try
            {
                var client = new WebClient();
                client.Headers.Add("User-Agent", "PERCULUS_SDK");
                client.Headers.Add("Authorization", "Bearer " + token);
                var content = client.UploadString(ApiUri(url, method), "DELETE", "");

                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                //WriteLog(ex.Message);
                T obj = default(T);
                return obj;
            }
        }


        private static Uri ApiUri(string url, string method)
        {
            if (!method.StartsWith('/') && !url.EndsWith('/'))
                url += '/';
            return new Uri(url + method);
        }

        public static string GetQueryString(object obj)
        {
            if (obj == null)
                return "";

            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        public static void WriteLog(string error)
        {
            StreamWriter file = File.AppendText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "perculus-xsdk-log.txt"));
            file.WriteLine(DateTime.Now.ToString() + " " + error);
            file.Close();
        }

    }
}
