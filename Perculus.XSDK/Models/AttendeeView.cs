using System;
using Perculus.XSDK.Models.Enum;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class AttendeeView
    {
        public string session_id { get; set; }
        public string user_id { get; set; }
        public string attendee_id { get; set; }
        public string attendance_code { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string mobile { get; set; }
        public string avatar { get; set; }
        public DateTime? creation_date { get; set; }
        public DateTime? updating_date { get; set; }
    }

}
