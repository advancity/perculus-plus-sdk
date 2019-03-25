using System;
using Perculus.XSDK.Models.Enum;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class ValidationResponse
    {
        public string Field { get; set; }
        public string Message { get; set; }
        public string Key { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
