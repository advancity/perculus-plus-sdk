using System;
using Perculus.XSDK.Models.Enum;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class SessionView
    {
        public string session_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tags { get; set; }
        public DateTimeOffset start_date { get; set; }
        public int duration { get; set; }
        public string lang { get; set; }
        public ActiveStatus status { get; set; }
        public ReplayStatus replay_status { get; set; }
        public string ui_options { get; set; }
        public DateTimeOffset? creation_date { get; set; }
        public DateTimeOffset? updating_date { get; set; }
    }
}
