using System.Threading.Tasks;

namespace DistanceBetweenAirports.Services
{
    public interface IProcessingService
    {
        Task<double> GetDistanceBetweenAirports(string from, string to);
    }
}
