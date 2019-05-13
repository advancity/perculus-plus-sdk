using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class SessionFilter
    {
        public string session_id { get; set; }
        public string session_name { get; set; }
        public string description { get; set; }
        public string tags { get; set; }
        public int? duraiton { get; set; }
        public DateTime? begin_date { get; set; }
        public DateTime? end_date { get; set; }
        public int page_number { get; set; }
        public int page_size { get; set; }
    }
}
