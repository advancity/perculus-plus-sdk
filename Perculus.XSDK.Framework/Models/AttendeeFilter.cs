using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class AttendeeFilter
    {
        public string UserId { get; set; }
        public string AttendanceCode { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Mobile { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
