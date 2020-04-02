using MadCill.DataExchangeAPIProvider.Endpoints;
using Newtonsoft.Json;
using Sitecore.DataExchange.Attributes;
using Sitecore.DataExchange.Contexts;
using Sitecore.DataExchange.Models;
using Sitecore.DataExchange.Plugins;
using Sitecore.DataExchange.Processors.PipelineSteps;
using Sitecore.Services.Core.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace MadCill.DataExchangeAPIProvider.Pipelines
{
    [RequiredEndpointPlugins(typeof(JsonApiEndpointSettings))]
    public class ReadFromJsonApiStepProcessor : BaseReadDataStepProcessor
    {
        public ReadFromJsonApiStepProcessor()
        {
        }
        protected override void ReadData(
            Endpoint endpoint,
            PipelineStep pipelineStep,
            PipelineContext pipelineContext,
            ILogger logger)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }
            if (pipelineStep == null)
            {
                throw new ArgumentNullException(nameof(pipelineStep));
            }
            if (pipelineContext == null)
            {
                throw new ArgumentNullException(nameof(pipelineContext));
            }
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }
            //
            //get the file path from the plugin on the endpoint
            var settings = endpoint.GetPlugin<JsonApiEndpointSettings>();
            if (settings == null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(settings.EndpointURL))
            {
                logger.Error(
                    "No endpoint URL is specified on the endpoint. " +
                    "(pipeline step: {0}, endpoint: {1})",
                    pipelineStep.Name, endpoint.Name);
                return;
            }

            Uri validatedUri = null;
            if (!Uri.TryCreate(settings.EndpointURL, UriKind.RelativeOrAbsolute, out validatedUri))
            {
                logger.Error(
                    "The endpoint URL is not a valid URL structure. " +
                    "(pipeline step: {0}, endpoint: {1})",
                    pipelineStep.Name, endpoint.Name);
                return;
            }
            //
            //add the data that was read from the file to a plugin
            var data = this.GetIterableData(settings, logger);

            //foreach(var d in data)
            //{
            //    logger.Info($"ReadFromJsonApiStepProcessor.GetIterableData {d["id"]}");
            //}

            var dataSettings = new IterableDataSettings(data);
            //
            //add the plugin to the pipeline context
            pipelineContext.AddPlugin(dataSettings);
        }
        protected virtual IEnumerable<Dictionary<string, string>> GetIterableData(JsonApiEndpointSettings settings, ILogger logger)
        {

            HttpClient client = new HttpClient();

            try
            {
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(settings.EndpointURL).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body.
                    var data = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(data))
                    {
                        var dict = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);
                        logger.Info("ReadFromJsonApiStepProcessor.GetIterableData Successfully retreived");
                        return dict;
                    }
                }
                else
                {
                    logger.Warn("ReadFromJsonApiStepProcessor.GetIterableData API call failed - {0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch(HttpException ex)
            {
                logger.Error("ReadFromJsonApiStepProcessor.GetIterableData API call exception - {0}", ex.Message, ex);
            }
            finally
            {
                //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                client.Dispose();
            }

            return null;
        }
    }
}