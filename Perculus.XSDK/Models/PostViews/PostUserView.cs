using Perculus.XSDK.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Perculus.XSDK.Models.PostViews
{
    public class PostUserView
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string mobile { get; set; }
        public bool login_allowed { get; set; }
        public UserActiveStatus? status { get; set; }
        public string lang { get; set; }
        public string timezone { get; set; }
        public float? timezone_offset { get; set; }
        public DateTimeOffset? expires_at { get; set; }
    }

}
