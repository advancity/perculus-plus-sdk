using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Perculus.XSDK.Extensions;

namespace Perculus.XSDK
{
    internal class HttpWebClient
    {
        public static Dictionary<string, string> DefaultHeaders { get; set; }
        public static string LogFilePath { get; set; }
        public static HttpWebRequest CreateWebRequest(string method, string uri, string contentType = "application/json")
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);

            webrequest.Method = method;

            if (!string.IsNullOrEmpty(contentType))
                webrequest.ContentType = contentType;

            if (DefaultHeaders != null && DefaultHeaders.Count > 0)
            {

                foreach (var header in DefaultHeaders)
                {
                    try
                    {
                        if (header.Key == "Content-Type" && !string.IsNullOrEmpty(contentType)) continue;

                        if (header.Key == "Accept")
                            webrequest.Headers.Add(HttpRequestHeader.Accept, header.Value);
                        else
                            webrequest.Headers.Add(header.Key, header.Value);
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            return webrequest;
        }

        public static HttpWebResponse SendWebRequest(HttpWebRequest request, object body = null)
        {
            if (request == null) return null;

            string strBody = "";
            if (body != null)
            {
                if (request.ContentType.Contains("x-www-form-urlencoded"))
                {
                    var formParameters = body.ToKeyValue();
                    if (formParameters != null)
                        strBody = string.Join("&", formParameters.Keys.Select(key => WebUtility.UrlEncode(key) + "=" + WebUtility.UrlEncode(formParameters[key])));
                }
                else if (request.ContentType.Contains("json"))
                    strBody = JsonConvert.SerializeObject(body);

            }
            if (!string.IsNullOrEmpty(strBody))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(strBody);
                request.ContentLength = bytes.Length;
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(strBody);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                return (HttpWebResponse)e.Response;
            }
        }

        public static string GetResponseBody(HttpWebResponse response)
        {
            var responseBody = String.Empty;

            if (response != null)
            {
                var stream = response.GetResponseStream();
                if (stream != null)
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        responseBody = streamReader.ReadToEnd();
                    }
                }
            }

            return responseBody;
        }

        #region
        public static T GET<T>(string url, string method, string token, object query = null)
        {
            try
            {
                var client = GetWebClient(token);
                var uri = ApiUri(url, $"{method}?{GetQueryString(query)}");
                var content = client.DownloadString(uri);
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                T obj = default(T);
                return obj;
            }
        }



        public static T POST<T>(string url, string method, string token, object data)
        {
            try
            {
                var client = GetWebClient(token);
                var content = client.UploadString(ApiUri(url, method), "POST", JsonConvert.SerializeObject(data));
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                T obj = default(T);
                return obj;
            }
        }



        public static T POST_FORMDATA<T>(string Url, object data)
        {
            try
            {
                var client = GetWebClient(null, null);
                var content = client.UploadString(Url, "POST", GetQueryString(data));
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                T obj = default(T);
                return obj;
            }
        }


        public static T PUT<T>(string url, string method, string token, object data)
        {
            try
            {
                var client = GetWebClient(token);
                var content = client.UploadString(ApiUri(url, method), "PUT", JsonConvert.SerializeObject(data));
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                T obj = default(T);
                return obj;
            }
        }

        public static T DELETE<T>(string url, string method, string token)
        {
            try
            {
                var client = GetWebClient(token);
                var content = client.UploadString(ApiUri(url, method), "DELETE", "");
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                T obj = default(T);
                return obj;
            }
        }

        private static WebClient GetWebClient(string authToken = null, string contentType = "application/json")
        {
            var client = new WebClient();
            client.Headers.Add("User-Agent", "PERCULUS_SDK");

            if (!String.IsNullOrEmpty(contentType))
            {
                client.Headers[HttpRequestHeader.ContentType] = contentType;
            }

            if (!String.IsNullOrEmpty(authToken))
            {
                client.Headers.Add("Authorization", "Bearer " + authToken);
            }

            return client;
        }

        private static Uri ApiUri(string url, string method)
        {
            if (!method.StartsWith("/") && !url.EndsWith("/"))
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
            if (!String.IsNullOrEmpty(LogFilePath))
            {
                try
                {
                    using (StreamWriter file = File.AppendText(LogFilePath))
                    {
                        file.WriteLine(DateTime.Now.ToString() + " " + error);
                    }
                }
                catch
                {
                    // 
                }
            }

        }
        #endregion
    }
}
