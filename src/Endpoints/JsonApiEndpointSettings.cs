using Sitecore.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MadCill.DataExchangeAPIProvider.Endpoints
{
    public class JsonApiEndpointSettings : IPlugin
    {
        public JsonApiEndpointSettings() { }
        public string EndpointURL { get; set; }
    }
}