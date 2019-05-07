using Newtonsoft.Json;
using Perculus.XSDK.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Perculus.XSDK.Extensions
{
    public static class HttpWebResponseExtensions
    {
        public static ApiErrorResponse ToErrorResponse(this HttpWebResponse response, string content = null)
        {
            ApiErrorResponse apiErrorResponse = null;

            if (response is null)
            {
                return null;
            }

            try
            {
                if (content is null)
                {
                    content = HttpWebClient.GetResponseBody(response);
                }
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    apiErrorResponse = new ApiErrorResponse(ApiErrorCode.Unauthorized);
                }
                else
                {
                    apiErrorResponse = content.ToObject<ApiErrorResponse>();
                }
            }
            finally
            {
                if (apiErrorResponse is null)
                {
                    apiErrorResponse = new ApiErrorResponse(ApiErrorCode.None);
                }
                apiErrorResponse.HttpStatusCode = response.StatusCode;
            }
            return apiErrorResponse;
        }
    }
}
