using Perculus.XSDK.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Perculus.XSDK.Models.PostViews
{
    public class PostSessionView
    {
        public string name { get; set; }
        public string description { get; set; }
        public string tags { get; set; }
        public DateTimeOffset start_date { get; set; }
        public int duration { get; set; }
        public string lang { get; set; }
        public SessionActiveStatus? status { get; set; }
        public string ui_options { get; set; }
    }
}

