using DistanceBetweenAirports.Models;
using DistanceBetweenAirports.Views;
using System.Threading.Tasks;

namespace DistanceBetweenAirports.Services
{
    public interface IGetAirportInfoService
    {
        Task<ResultData<AirportInfoDto>> GetAirportInfoAsync(string airportCode);  
    }
}
