using System.Threading.Tasks;

namespace WundergroundNetLib.Interfaces
{
    public interface IDataProvider
    {
        Task<WeatherData> GetCombinedDataAsync(string stationID);
        Task<WeatherData> GetCombinedDataAsync(string latitude, string longitude);
        Task<WeatherData> GetCombinedDataAsync(double latitude, double longitude);
    }
}