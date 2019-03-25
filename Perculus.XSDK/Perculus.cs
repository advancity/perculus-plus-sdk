using Perculus.XSDK.Components;
using Perculus.XSDK.Extensions;
using Perculus.XSDK.Models;
using System;
using System.Linq;
using System.IO;
using System.Net;

namespace Perculus.XSDK
{
    public class Perculus
    {

        PerculusOptions Options { get; set; }
        //COMPONENTS
        public Users Users = null;
        public Sessions Sessions = null;
        public Attendees Attendees = null;

        public Perculus()
        {
            Options = new PerculusOptions();

            // read setting values from Environment
            Options.GetType().GetProperties().ToList().ForEach(p =>
            {
                // like PAPI_USERNAME or PAPI_PASSWORD
                var envPropName = "PAPI_" + p.Name;
                var envPropValue = Environment.GetEnvironmentVariable(envPropName);
                if (!String.IsNullOrEmpty(envPropValue))
                {
                    p.SetValue(Options, envPropValue);
                }
            });
        }

        public Perculus(PerculusOptions options) : this()
        {
            if (options != null)
            {
                // merge the given options with Options. Override that were read from Environment.
                options.GetType().GetProperties().ToList().ForEach(p =>
                {
                    var val = p.GetValue(options) as string;
                    if (!String.IsNullOrEmpty(val))
                    {
                        var mainProp = Options.GetType().GetProperty(p.Name);
                        if (mainProp != null)
                        {
                            mainProp.SetValue(Options, val);
                        }
                    }
                });
            }
            InitializeSdk();
        }

        public Perculus(Uri api_uri, Uri auth_uri, string account_id, string username, string password, string logFilePath) :
        this(new PerculusOptions
        {
            API_URI = api_uri.AbsoluteUri,
            AUTH_URI = auth_uri.AbsoluteUri,
            ACCOUNT_ID = account_id,
            USERNAME = username,
            PASSWORD = password,
            LOG_FILE_PATH = logFilePath
        })
        { }

        private void InitializeSdk()
        {
            HttpWebClient.DefaultHeaders = new System.Collections.Generic.Dictionary<string, string>();
            HttpWebClient.DefaultHeaders.Add("Accept", "application/json");
            HttpWebClient.DefaultHeaders.Add("Content-Type", "application/json");
            HttpWebClient.LogFilePath = Options.LOG_FILE_PATH;

            if (!String.IsNullOrEmpty(HttpWebClient.LogFilePath) && !Path.IsPathFullyQualified(HttpWebClient.LogFilePath))
            {
                HttpWebClient.LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HttpWebClient.LogFilePath);
            }

            // access token might have been provided explicitly
            // if so, don't go to server...
            if (String.IsNullOrEmpty(Options.ACCESS_TOKEN))
            {
                Options.ACCESS_TOKEN = GetAuthToken();
            }

            HttpWebClient.DefaultHeaders.Add("Authorization", "Bearer " + Options.ACCESS_TOKEN);

            Sessions = new Sessions(Options);
            Users = new Users(Options);
            Attendees = new Attendees(Options);
        }

        /// <summary>
        /// Get the authentication token from the auth server
        /// </summary>
        /// <returns></returns>
        private string GetAuthToken()
        {
            AuthRequest AuthData = new AuthRequest()
            {
                account_id = Options.ACCOUNT_ID,
                username = Options.USERNAME,
                password = Options.PASSWORD,
                client_id = "api",
                grant_type = "password",
            };

            var request = HttpWebClient.CreateWebRequest("POST", Options.AUTH_URI, "application/x-www-form-urlencoded");
            var response = HttpWebClient.SendWebRequest(request, AuthData);
            AuthResponse authResponse = null;
            if (response != null)
            {
                string result = HttpWebClient.GetResponseBody(response);
                authResponse = result.ToObject<AuthResponse>();
            }
            return authResponse != null ? authResponse.access_token : "";
        }

    }
}
