using Perculus.XSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.ExampleApp
{
    internal class Common
    {
        private static Perculus _perculusClient = null;

        public static Perculus CreatePerculusClient(bool createNewInstance = false)
        {
            Perculus client = null;

            if (!createNewInstance && _perculusClient != null)
            {
                return _perculusClient;
            }

            var config = Config.GetInstance();
            client = new Perculus(new Uri(config.API_URL), new Uri(config.AUTH_URL), config.ACCOUNT_ID, config.USERNAME, config.PASSWORD, config.LOG_FILE_PATH);

            if (!createNewInstance)
            {
                _perculusClient = client;
            }

            return client;
        }

        public static void HandleErrorResponse(ApiErrorResponse response)
        {
            Console.Out.WriteLine($"API Error: {response.Code}\nHTTP Status Code: {response.HttpStatusCode}\nDetails:{response.Details}");
        }
    }
}
