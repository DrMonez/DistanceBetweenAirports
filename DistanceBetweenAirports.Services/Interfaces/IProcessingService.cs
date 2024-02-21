using DistanceBetweenAirports.Core.Models;

namespace DistanceBetweenAirports.Services.Interfaces
{
    public interface IProcessingService
    {
        Task<ResultData<double>> GetDistanceBetweenAirportsInMiles(string from, string to);
    }
}
