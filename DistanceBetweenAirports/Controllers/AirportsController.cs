using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DistanceBetweenAirports.Services;

namespace DistanceBetweenAirports.Controllers
{
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

        [HttpGet]
        [Route("distance_in_miles")]
        public async Task<double> GetDistanceBetweenAirportsInMiles(string from, string to)
        {
            if(!IsAirportCodeValid(from))
            {
                _logger.LogError("Airport code should not be null and contains only 3 letters (from)");
            }
            if(!IsAirportCodeValid(to))
            {
                _logger.LogError("Airport code should not be null and contains only 3 letters (to)");
            }
            // TODO: change to message result with status and error message
            return await _processingService.GetDistanceBetweenAirports(from, to);
        }

        private bool IsAirportCodeValid(string code)
        {
            if(code == null || code.Trim().Length != 3)
            {
                return false;
            }
            return true;
        }
    }
}
