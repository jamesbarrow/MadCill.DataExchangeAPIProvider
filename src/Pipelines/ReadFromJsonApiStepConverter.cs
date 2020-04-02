using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Converters.PipelineSteps;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Repositories;
using Sitecore.Services.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MadCill.DataExchangeAPIProvider.Pipelines
{
    [SupportedIds(ReadFromJsonApiStepTemplateId)]
    public class ReadFromJsonApiStepConverter : BasePipelineStepConverter
    {
        public const string ReadFromJsonApiStepTemplateId = Templates.ReadFromJsonApiStepTemplate.IDValue;
        public const string TemplateFieldEndpointFrom = "EndpointFrom";
        public ReadFromJsonApiStepConverter(IItemModelRepository repository) : base(repository)
        {
        }
        protected override void AddPlugins(ItemModel source, PipelineStep pipelineStep)
        {
            //
            //create the plugin
            var settings = new EndpointSettings
            {
                //
                //populate the plugin using values from the item
                EndpointFrom = this.ConvertReferenceToModel<Endpoint>(source, TemplateFieldEndpointFrom)
            };
            //
            //add the plugin to the pipeline step
            pipelineStep.AddPlugin(settings);
        }
    }
}