using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class PerculusOptions
    {
        public string API_URI { get; set; }
        public string AUTH_URI { get; set; }
        public string ACCOUNT_ID { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string ACCESS_TOKEN { get; set; }
        public string LOG_FILE_PATH { get; set; }
    }
}
