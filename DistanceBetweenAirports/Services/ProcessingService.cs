using DistanceBetweenAirports.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DistanceBetweenAirports.Services
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
            if (from == null || to == null)
            {
                _logger.LogError(Constants.GetNullValidationMessage("Airport code"));
                return new ResultData<double> { error = Constants.GetNullValidationMessage("Airport code") };
            }
            var fromAirportInfo = await GetAirportInfo(from);
            _logger.LogInformation("From Airport Info: " + JsonConvert.SerializeObject(fromAirportInfo));
            var toAirportInfo = await GetAirportInfo(to);
            _logger.LogInformation("To Airport Info: " + JsonConvert.SerializeObject(toAirportInfo));
            
            if (fromAirportInfo.error != null || toAirportInfo.error != null)
            {
                return new ResultData<double> { error = string.Join(";", fromAirportInfo.error, toAirportInfo.error) };
            }
            return CalculateDistance(fromAirportInfo.result.Location, toAirportInfo.result.Location, Constants.EARTH_RADIUS_IN_MILES);
        }

        private ResultData<double> CalculateDistance(Location from, Location to, double radius)
        {
            if (from == null || to == null)
            {
                _logger.LogError(Constants.GetNullValidationMessage("Locations"));
                return new ResultData<double> { error = Constants.GetNullValidationMessage("Locations") };
            }
            var fromLatitudeInRadians = from.Latitude * Math.PI / 180;
            var toLatitudeInRadians = to.Latitude * Math.PI / 180;
            var deltaLatitudeInRadians = (to.Latitude - from.Latitude) * Math.PI / 180;
            var deltaLongitudeInRadians = (to.Longitude - from.Longitude) * Math.PI / 180;

            var haversine = Math.Sin(deltaLatitudeInRadians / 2) * Math.Sin(deltaLatitudeInRadians / 2) +
                Math.Cos(fromLatitudeInRadians) * Math.Cos(toLatitudeInRadians) *
                Math.Sin(deltaLongitudeInRadians / 2) * Math.Sin(deltaLongitudeInRadians / 2);

            return new ResultData<double> { result = 2 * Math.Atan2(Math.Sqrt(haversine), Math.Sqrt(1 - haversine)) * radius };
        }

        private async Task<ResultData<AirportInfo>> GetAirportInfo(string code)
        {
            var normalizedCode = NormalizeAirportCode(code);
            var airportInfoDto = await _getAirportInfoService.GetAirportInfoAsync(normalizedCode);
            return new ResultData<AirportInfo> { error = airportInfoDto.error, result = airportInfoDto.result?.ToModel() };
        }

        private string NormalizeAirportCode(string code)
        {
            return code.Trim().ToUpper().Substring(0, 3);
        }
    }
}
