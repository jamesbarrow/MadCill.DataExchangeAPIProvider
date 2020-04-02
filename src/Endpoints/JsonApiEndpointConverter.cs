using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.Endpoints;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MadCill.DataExchangeAPIProvider.Endpoints
{
    [SupportedIds(JsonApiEndpointTemplateId)]
    public class JsonApiEndpointConverter : BaseEndpointConverter
    {
        public const string JsonApiEndpointTemplateId = Templates.JsonApiEndpointTemplate.IDValue;
        public const string TemplateFieldEndpointURL = "EndpointURL";

        public JsonApiEndpointConverter(IItemModelRepository repository) : base(repository)
        {
        }

        protected override void AddPlugins(ItemModel source, Endpoint endpoint)
        {
            //create the plugin
            var settings = new JsonApiEndpointSettings
            {
                EndpointURL = this.GetStringValue(source, TemplateFieldEndpointURL)
            };

            //add the plugin to the endpoint
            endpoint.AddPlugin(settings);
        }
    }
}