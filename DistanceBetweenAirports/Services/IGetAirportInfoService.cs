using DistanceBetweenAirports.Views;
using System.Threading.Tasks;

namespace DistanceBetweenAirports.Services
{
    public interface IGetAirportInfoService
    {
        Task<AirportInfoDto> GetAirportInfoAsync(string airportCode);  
    }
}
