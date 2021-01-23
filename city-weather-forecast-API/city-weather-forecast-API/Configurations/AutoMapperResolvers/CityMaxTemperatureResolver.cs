using AutoMapper;
using city_weather_forecast_API.Models;
using city_weather_forecast_API.Models.DTO;
using city_weather_forecast_API.Services.ServiceInterfaces;

namespace city_weather_forecast_API.Configurations
{
    public class CityMaxTemperatureResolver : IValueResolver<City, CityDetailsDTO, string>
    {
        private readonly IForecastApi _forecastApiService;

        public CityMaxTemperatureResolver(IForecastApi forecastApiService)
        {
            _forecastApiService = forecastApiService;
        }

        public string Resolve(City source, CityDetailsDTO destination, string destMember, ResolutionContext context)
        {
            return _forecastApiService.GetTemperature(source.PlaceCode, Services.TemperatureExtremum.Max).Result;
        }
    }
}
