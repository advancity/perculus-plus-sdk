using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    class AuthResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public int FailedAttemptCount { get; set; }
        public int TotalAttemptCount { get; set; }
    }
}
