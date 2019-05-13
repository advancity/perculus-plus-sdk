﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class AttendeeFilter
    {
        public string user_id { get; set; }
        public string attendance_code { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string mobile { get; set; }
        public int page_size { get; set; }
        public int page_number { get; set; }
    }
}
