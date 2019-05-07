using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Configuration;

namespace Perculus.XSDK.ExampleApp
{
    internal class Config
    {
        private static Config _config;

        private Config()
        {
            var appSettings = ConfigurationManager.AppSettings;

            var baseUrl = appSettings["base_url"];
            API_URL = AddBaseUrl(appSettings["api_url"], baseUrl);
            AUTH_URL = AddBaseUrl(appSettings["auth_url"], baseUrl);
            APP_JOIN_URL_FORMAT = AddBaseUrl(appSettings["app_join_url_format"], baseUrl);

            ACCOUNT_ID = appSettings["account_id"];
            USERNAME = appSettings["username"];
            PASSWORD = appSettings["password"];
            LOG_FILE_PATH = appSettings["sdk_log_file_path"];
        }

        private string AddBaseUrl(string v, string baseUrl)
        {
            if (!v.StartsWith("http"))
            {
                return baseUrl.TrimEnd('/') + (!v.StartsWith("/") ? "/" : "") + v;
            }
            return v;
        }

        public static Config GetInstance()
        {
            if (_config == null)
            {
                _config = new Config();
            }
            return _config;
        }

        public readonly string API_URL;
        public readonly string AUTH_URL;
        public readonly string APP_JOIN_URL_FORMAT;
        public readonly string ACCOUNT_ID;
        public readonly string USERNAME;
        public readonly string PASSWORD;
        public readonly string LOG_FILE_PATH;
    }
}
