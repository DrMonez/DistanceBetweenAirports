using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DistanceBetweenAirports.Services;
using DistanceBetweenAirports.Models;

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
        public async Task<ResultData<double>> GetDistanceBetweenAirportsInMiles(string from, string to)
        {
            // TODO: add data cache
            if(!IsValidAirportCode(from))
            {
                _logger.LogError(Constants.UNCORRECT_AIRPORT_CODE_MESSAGE + " (from)");
                return new ResultData<double> { error = Constants.UNCORRECT_AIRPORT_CODE_MESSAGE + " (from)" };
            }
            if(!IsValidAirportCode(to))
            {
                _logger.LogError(Constants.UNCORRECT_AIRPORT_CODE_MESSAGE + " (to)");
                return new ResultData<double> { error = Constants.UNCORRECT_AIRPORT_CODE_MESSAGE + " (to)" };
            }
            return await _processingService.GetDistanceBetweenAirportsInMiles(from, to);
        }

        private bool IsValidAirportCode(string code)
        {
            if(code == null || code.Trim().Length != 3)
            {
                return false;
            }
            return true;
        }
    }
}
