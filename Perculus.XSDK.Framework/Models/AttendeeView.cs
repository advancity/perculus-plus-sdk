using System;
using Perculus.XSDK.Models.Enum;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class AttendeeView
    {
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string AttendeeId { get; set; }
        public string AttendanceCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Mobile { get; set; }
        public string Avatar { get; set; }
    }

}
