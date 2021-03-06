using DistanceBetweenAirports.Models;
using DistanceBetweenAirports.Views;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace DistanceBetweenAirports.Services
{
    public class GetAirportInfoService: IGetAirportInfoService
    {
        private readonly ILogger<GetAirportInfoService> _logger;
        private readonly HttpClient _client;
        private readonly string _providerUrl;

        public GetAirportInfoService(ILogger<GetAirportInfoService> logger, IHttpClientFactory httpClientFactory, string providerUrl = Constants.DEFAULT_PROVIDER_URL)
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
                _logger.LogError("External API returned error: " + response.StatusCode.ToString());
                return new ResultData<AirportInfoDto> { error = "External API returned error: " + response.StatusCode.ToString() };
            }
            return new ResultData<AirportInfoDto> { result = JsonConvert.DeserializeObject<AirportInfoDto>(await response.Content.ReadAsStringAsync()) };
        }
    }
}
