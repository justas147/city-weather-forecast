using city_weather_forecast_API.Models;
using System.Threading.Tasks;

namespace city_weather_forecast_API.Services.ServiceInterfaces
{
    public interface IForecastApi
    {
        Task<string> GetTemperature(string CityCode, TemperatureExtremum extremumType);
        Task<CitySelection[]> GetAllPlaces();
    }
}
