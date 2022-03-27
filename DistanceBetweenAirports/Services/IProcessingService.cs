using DistanceBetweenAirports.Models;
using System.Threading.Tasks;

namespace DistanceBetweenAirports.Services
{
    public interface IProcessingService
    {
        Task<ResultData<double>> GetDistanceBetweenAirportsInMiles(string from, string to);
    }
}
