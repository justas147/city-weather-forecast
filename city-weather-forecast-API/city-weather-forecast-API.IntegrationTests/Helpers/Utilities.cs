using city_weather_forecast_API.Database;
using city_weather_forecast_API.Models;
using System.Collections.Generic;

namespace city_weather_forecast_API.IntegrationTests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(CityDbContext db)
        {
            db.CityItem.AddRange(GetSeedingCities());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(CityDbContext db)
        {
            db.CityItem.RemoveRange(db.CityItem);
            InitializeDbForTests(db);
        }

        public static List<City> GetSeedingCities()
        {
            return new List<City>()
            {
                new City()
                {
                    PlaceCode = "kaunas",
                    Name = "Kaunas",
                    Description = "My city"
                },
                new City()
                {
                    PlaceCode = "vilnius",
                    Name = "Vilnius",
                    Description = "Capital city"
                }
            };
        }
    }
}
