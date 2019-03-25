using System;
using Perculus.XSDK.Models.Enum;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class AttendeesPostResult
    {
        public AttendeesPostResult()
        {
            approved = new List<AttendeeView>();
            rejected = new List<MultipleResponseView>();
        }
        public List<AttendeeView> approved { get; set; }
        public List<MultipleResponseView> rejected { get; set; }
    }
}
