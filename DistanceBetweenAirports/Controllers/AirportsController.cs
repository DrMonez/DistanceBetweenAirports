using DistanceBetweenAirports.API.Helpers;
using DistanceBetweenAirports.Core.Models;
using DistanceBetweenAirports.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DistanceBetweenAirports.API.Controllers
{
    /// <summary>
    /// Airports controller.
    /// </summary>
    [ApiController]
    [Route("api/airports")]
    public class AirportsController : ControllerBase
    {
        private readonly ILogger<AirportsController> _logger;
        private readonly IProcessingService _processingService;

        public AirportsController(ILogger<AirportsController> logger, IProcessingService processingService)
        {
            _logger = logger;
            _processingService = processingService;
        }

        /// <summary>
        /// Get distance between two airports.
        /// </summary>
        /// <param name="from">3-letter IATA code of departure airport.</param>
        /// <param name="to">3-letter IATA code of arrival airport.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("distance_in_miles")]
        public async Task<ResultData<double>> GetDistanceBetweenAirportsInMiles(string from, string to)
        {
            // TODO: add external data cache (e.g. Redis)
            if (!ValidationHelper.IsValidAirportCode(from))
            {
                var message = Constants.UncorrectAirportCodeMessage + " (from)";
                _logger.LogError(message);
                return new ResultData<double> { Error = message };
            }
            if (!ValidationHelper.IsValidAirportCode(to))
            {
                var message = Constants.UncorrectAirportCodeMessage + " (to)";
                _logger.LogError(message);
                return new ResultData<double> { Error = message };
            }
            return await _processingService.GetDistanceBetweenAirportsInMiles(from, to);
        }
    }
}
