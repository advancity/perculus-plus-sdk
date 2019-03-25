using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Extensions
{
    public static class StringExtensions
    {
        public static T ToObject<T>(this string content)
        {
            try
            {
                if (!string.IsNullOrEmpty(content))
                    return JsonConvert.DeserializeObject<T>(content);
                else
                    return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
