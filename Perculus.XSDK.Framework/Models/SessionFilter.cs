using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class SessionFilter
    {
        public string SessionId { get; set; }
        public string SessionName { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int? Duration { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
