using DistanceBetweenAirports.Core.Models;
using DistanceBetweenAirports.Services.Views;

namespace DistanceBetweenAirports.Services.Interfaces
{
    public interface IGetAirportInfoService
    {
        Task<ResultData<AirportInfoDto>> GetAirportInfoAsync(string airportCode);
    }
}
