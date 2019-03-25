using Perculus.XSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.ExampleApp
{
    internal class Common
    {
        public static Perculus CreatePerculusClient()
        {
            var config = Config.GetInstance();
            Perculus perculus = new Perculus(new Uri(config.API_URL), new Uri(config.AUTH_URL), config.ACCOUNT_ID, config.USERNAME, config.PASSWORD, config.LOG_FILE_PATH);
            return perculus;
        }

        public static void HandleErrorResponse(ApiErrorResponse response)
        {
            Console.Out.WriteLine($"API Error: {response.Code}\nHTTP Status Code: {response.HttpStatusCode}\nDetails:{response.Details}");
        }
    }
}
