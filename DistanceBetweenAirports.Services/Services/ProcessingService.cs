using DistanceBetweenAirports.Core.Models;
using DistanceBetweenAirports.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DistanceBetweenAirports.Services.Services
{
    public class ProcessingService : IProcessingService
    {
        private readonly ILogger<ProcessingService> _logger;
        private readonly IGetAirportInfoService _getAirportInfoService;

        public ProcessingService(ILogger<ProcessingService> logger, IGetAirportInfoService getAirportInfoService)
        {
            _logger = logger;
            _getAirportInfoService = getAirportInfoService;
        }

        public async Task<ResultData<double>> GetDistanceBetweenAirportsInMiles(string from, string to)
        {
            // TODO: normalization & validation layers
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                var message = Constants.GetNullValidationMessage("Airport code");
                _logger.LogError(message);
                return new ResultData<double> { Error = message };
            }
            var fromAirportInfo = await GetAirportInfo(from);
            _logger.LogInformation("From Airport Info: " + JsonConvert.SerializeObject(fromAirportInfo));
            var toAirportInfo = await GetAirportInfo(to);
            _logger.LogInformation("To Airport Info: " + JsonConvert.SerializeObject(toAirportInfo));

            if (fromAirportInfo.Error != null || toAirportInfo.Error != null)
            {
                return new ResultData<double> { Error = string.Join(";", fromAirportInfo.Error, toAirportInfo.Error) };
            }
            return CalculateDistance(fromAirportInfo.Result?.Location, toAirportInfo.Result?.Location, Constants.EarthRadiusInMiles);
        }

        private ResultData<double> CalculateDistance(Location? from, Location? to, double radius)
        {
            if (from == null || to == null)
            {
                var message = Constants.GetNullValidationMessage("Locations");
                _logger.LogError(message);
                return new ResultData<double> { Error = message };
            }
            var fromLatitudeInRadians = from.Latitude * Math.PI / 180;
            var toLatitudeInRadians = to.Latitude * Math.PI / 180;
            var deltaLatitudeInRadians = (to.Latitude - from.Latitude) * Math.PI / 180;
            var deltaLongitudeInRadians = (to.Longitude - from.Longitude) * Math.PI / 180;

            var haversine = Math.Sin(deltaLatitudeInRadians / 2) * Math.Sin(deltaLatitudeInRadians / 2) +
                Math.Cos(fromLatitudeInRadians) * Math.Cos(toLatitudeInRadians) *
                Math.Sin(deltaLongitudeInRadians / 2) * Math.Sin(deltaLongitudeInRadians / 2);

            return new ResultData<double> { Result = 2 * Math.Atan2(Math.Sqrt(haversine), Math.Sqrt(1 - haversine)) * radius };
        }

        private async Task<ResultData<AirportInfo?>> GetAirportInfo(string code)
        {
            var normalizedCode = NormalizeAirportCode(code);
            var airportInfoDto = await _getAirportInfoService.GetAirportInfoAsync(normalizedCode);
            return new ResultData<AirportInfo?> { Error = airportInfoDto.Error, Result = airportInfoDto.Result?.ToModel() };
        }

        private string NormalizeAirportCode(string code)
        {
            return code.Trim().ToUpper().Substring(0, 3);
        }
    }
}
