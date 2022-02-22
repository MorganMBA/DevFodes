using DevFodes.CMS.Business.Client;
using DevFodes.CMS.Business.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace DevFodes.CMS.Infrastructure.Client
{
    public class HubSpotClient : IHubSpotClient
    {

        private readonly RestClient _client;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly string _baseAddress;
        private readonly string _apiKey;
        private readonly string _token;


        public HubSpotClient(IConfiguration configuration,
            ILogger<HubSpotClient> logger)
        {
           _configuration = configuration;

           _baseAddress = _configuration["HubSpot:HubSpotEndpoint"]
                ?? throw new ArgumentNullException("AppSettings:HubSpotEndpoint");

            _apiKey = _configuration["HubSpot:HubSpotApiKey"]
                ?? throw new ArgumentNullException("AppSettings:HubSpotApiKey");

            _token = _configuration["HubSpot:HubSpotToken"]
                ?? throw new ArgumentNullException("AppSettings:HubSpotToken");

            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<string> PostContactAsync(Contact serializedRequest)
        {
            _logger.LogInformation(nameof(PostContactAsync));
            _logger.LogInformation("Request serialized", serializedRequest);

            var endpoint = string.Format("{0}?", _baseAddress);

            var _client = new RestClient(endpoint);
            var bearer = string.Format("Bearer {0}", _token);

            var request = new RestRequest("/resource/", Method.Post);

            request.AddHeader("Content-Type:", "application/json");
            request.AddHeader("authorization:", bearer);

            request.AddParameter("application/json", serializedRequest, ParameterType.RequestBody);

            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                var status = response.StatusCode == System.Net.HttpStatusCode.BadRequest;

                throw new Exception($"An erro occured when calling HubSpot API ({_baseAddress})");
            }

            var responseContent = response.Content;

            return responseContent;
        }
    }
}
