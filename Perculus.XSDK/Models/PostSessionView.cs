using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class PostSessionView
    {
        public string name { get; set; }
        public string description { get; set; }
        public string tags { get; set; }
        public DateTimeOffset start_date { get; set; }
        public int duration { get; set; }
        public string lang { get; set; }
        public string ui_options { get; set; }
    }
}
