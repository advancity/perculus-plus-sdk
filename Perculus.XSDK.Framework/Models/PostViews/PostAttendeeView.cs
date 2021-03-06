﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Perculus.XSDK.Models.PostViews
{
    public class PostAttendeeView
    {
        public string user_id { get; set; }
        public string attendee_id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string mobile { get; set; }
        public string avatar { get; set; }
    }
}
