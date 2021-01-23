using AutoMapper;
using city_weather_forecast_API.Models;
using city_weather_forecast_API.Models.DTO;

namespace city_weather_forecast_API.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<City, CityDetailsDTO>()
                .ForMember(dest => dest.MaxTemperature, opt => opt.MapFrom<CityMaxTemperatureResolver>())
                .ForMember(dest => dest.MinTemperature, opt => opt.MapFrom<CityMinTemperatureResolver>());
        }
    }
}
