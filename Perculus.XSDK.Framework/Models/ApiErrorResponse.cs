using Perculus.XSDK.Models.Enum;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse()
        {
            Code = ApiErrorCode.None;
        }
        public ApiErrorResponse(ApiErrorCode code)
        {
            Code = code;
        }
        public ApiErrorCode Code { get; set; }
        public object Details { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }

    public class MultipleResponseView
    {
        public object Model { get; set; }
        public ApiErrorResponse State { get; set; }
    }
}
