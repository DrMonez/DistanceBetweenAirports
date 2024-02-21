using DistanceBetweenAirports.Core.Models;
using DistanceBetweenAirports.Services.Interfaces;
using DistanceBetweenAirports.Services.Views;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DistanceBetweenAirports.Services.Services
{
    public class GetAirportInfoService : IGetAirportInfoService
    {
        private readonly ILogger<GetAirportInfoService> _logger;
        private readonly HttpClient _client;
        private readonly string _providerUrl;

        public GetAirportInfoService(ILogger<GetAirportInfoService> logger, IHttpClientFactory httpClientFactory, string providerUrl = Constants.DefaultProviderUrl)
        {
            // TODO: add provider URL to environment variables
            _client = httpClientFactory.CreateClient();
            _providerUrl = providerUrl;
            _logger = logger;
        }

        public async Task<ResultData<AirportInfoDto>> GetAirportInfoAsync(string airportCode)
        {
            // TODO: add external api errors handling
            var response = await _client.GetAsync(_providerUrl + airportCode);
            if (!response.IsSuccessStatusCode)
            {
                var message = "External API returned error: " + response.StatusCode.ToString();
                _logger.LogError(message);
                return new ResultData<AirportInfoDto> { Error = message };
            }
            return new ResultData<AirportInfoDto> { Result = JsonConvert.DeserializeObject<AirportInfoDto>(await response.Content.ReadAsStringAsync()) };
        }
    }
}
