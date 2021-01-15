using city_weather_forecast_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace city_weather_forecast_API.Database
{
    public class CityDbContext : DbContext
    {
        public DbSet<City> CityItem { get; set; }

        public CityDbContext(DbContextOptions<CityDbContext> options) : base(options)
        {
        }
    }
}
