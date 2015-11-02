using System.Threading.Tasks;

namespace WundergroundNetLib.Interfaces
{
    public interface IDataProvider
    {
        Task<IWeatherData> GetCombinedDataAsync(string stationID);
        Task<IWeatherData> GetCombinedDataAsync(string latitude, string longitude);
        Task<IWeatherData> GetCombinedDataAsync(double latitude, double longitude);
    }
}