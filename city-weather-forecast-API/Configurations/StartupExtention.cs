using city_weather_forecast_API.Database;
using city_weather_forecast_API.Models;
using city_weather_forecast_API.Models.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace city_weather_forecast_API.Configurations
{
    public static class StartupExtention
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "city_weather_forecast_API", Version = "v1" });
            });

            return services;
        }

        public static void ConfigureAndUseSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "city_weather_forecast_API v1"));
        }

        public static void SetUpDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string ConnectionString = configuration["Database:ConnectionString"];
            services.AddDbContext<CityDbContext>(opt => opt.UseSqlite(ConnectionString));
        }

        public static void SetUpAutoMapper(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<City, CityDetailsDTO>();
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
