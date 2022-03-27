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

        public async Task<double> GetDistanceBetweenAirports(string from, string to)
        {
            if (from == null || to == null)
            {
                _logger.LogError(Constants.GetNullValidationMessage("Airport code"));
                return 0.0;
            }
            var fromAirportInfo = await GetAirportInfo(from);
            _logger.LogInformation("From Airport Info: " + JsonConvert.SerializeObject(fromAirportInfo));
            var toAirportInfo = await GetAirportInfo(to);
            _logger.LogInformation("To Airport Info: " + JsonConvert.SerializeObject(toAirportInfo));
            
            return CalculateDistance(fromAirportInfo.Location, toAirportInfo.Location);
        }

        private double CalculateDistance(Location from, Location to)
        {
            if (from == null || to == null)
            {
                _logger.LogError(Constants.GetNullValidationMessage("Locations"));
                return 0.0;
            }
            var fromLatitudeInRadians = from.Latitude * Math.PI / 180;
            var toLatitudeInRadians = to.Latitude * Math.PI / 180;
            var deltaLatitudeInRadians = (to.Latitude - from.Latitude) * Math.PI / 180;
            var deltaLongitudeInRadians = (to.Longitude - from.Longitude) * Math.PI / 180;

            var a = Math.Sin(deltaLatitudeInRadians / 2) * Math.Sin(deltaLatitudeInRadians / 2) +
                Math.Cos(fromLatitudeInRadians) * Math.Cos(toLatitudeInRadians) *
                Math.Sin(deltaLongitudeInRadians / 2) * Math.Sin(deltaLongitudeInRadians / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return c * Constants.EARTH_RADIUS_IN_MILES;
        }

        private async Task<AirportInfo> GetAirportInfo(string code)
        {
            var normalizedCode = NormalizeAirportCode(code);
            return (await _getAirportInfoService.GetAirportInfoAsync(normalizedCode)).ToModel();
        }

        private string NormalizeAirportCode(string code)
        {
            return code.Trim().ToUpper().Substring(0, 3);
        }
    }
}
