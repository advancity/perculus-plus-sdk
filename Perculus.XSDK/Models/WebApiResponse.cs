using Perculus.XSDK.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace Perculus.XSDK.Models
{
    public class WebApiResponse
    {

        private Stream _stream { get; set; }
        public HttpWebResponse Response { get; private set; }

        public WebApiResponse(HttpWebResponse response)
        {
            Response = response;
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return Response != null ? Response.StatusCode :  HttpStatusCode.NoContent;
            }
        }

        public Stream GetResponseStream()
        {
            if (_stream == null)
                _stream = Response.GetResponseStream().DeepClone();

            _stream.Position = 0;
            return _stream.DeepClone(false);
        }
    }
}
