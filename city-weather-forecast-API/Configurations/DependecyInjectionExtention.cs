using city_weather_forecast_API.Models;
using city_weather_forecast_API.Repositories;
using city_weather_forecast_API.Services;
using city_weather_forecast_API.Services.ServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace city_weather_forecast_API.Configurations
{
    public static class DependecyInjectionExtention
    {
        public static IServiceCollection AddAllDependencies(this IServiceCollection service)
        {
            return service
                .AddInfrastructureDependencies()
                .AddApplicationDependencies();
        }

        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection service)
        {
            return service
                .AddScoped<IRepository<City, string>, CityRepository>();
        }

        public static IServiceCollection AddApplicationDependencies(this IServiceCollection service)
        {
            return service
                .AddScoped<ICityService, CityService>()
                .AddScoped<IForecastApi, ForecastApiService>();
        }
    }
}
