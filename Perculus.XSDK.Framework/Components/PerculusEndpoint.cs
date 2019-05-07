using Perculus.XSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perculus.XSDK.Components
{
    public abstract class PerculusEndpoint
    {
        protected PerculusOptions Options { get; set; }
        public PerculusEndpoint(PerculusOptions options)
        {
            Options = options;
        }

        public string BuildRoute(string route = "")
        {
            if (!string.IsNullOrEmpty(route) && !route.StartsWith("/"))
                route = '/' + route;

            route = Options.API_URI + route;
            return route;
        }
    }
}
