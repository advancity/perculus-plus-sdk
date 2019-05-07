using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Perculus.XSDK.ExampleApp
{
    internal class Config
    {
        private static Config _config;

        private Config()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            // override settings with local settings
            // appsettings.local.json is ignored by git.
            if (File.Exists("appsettings.local.json")) { 
                builder.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
            }

            IConfigurationRoot configuration = builder.Build();

            var baseUrl = configuration.GetSection("AppSettings")["base_url"];
            API_URL = AddBaseUrl(configuration.GetSection("AppSettings")["api_url"], baseUrl);
            AUTH_URL = AddBaseUrl(configuration.GetSection("AppSettings")["auth_url"], baseUrl);
            APP_JOIN_URL_FORMAT = AddBaseUrl(configuration.GetSection("AppSettings")["app_join_url_format"], baseUrl);

            ACCOUNT_ID = configuration.GetSection("AppSettings")["account_id"];
            USERNAME = configuration.GetSection("AppSettings")["username"];
            PASSWORD = configuration.GetSection("AppSettings")["password"];
            LOG_FILE_PATH = configuration.GetSection("AppSettings")["sdk_log_file_path"];
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
