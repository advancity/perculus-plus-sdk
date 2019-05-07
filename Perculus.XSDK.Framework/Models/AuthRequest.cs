using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    class AuthRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string client_id { get; set; }
        public string grant_type { get; set; }
        public string account_id { get; set; }
    }
}
