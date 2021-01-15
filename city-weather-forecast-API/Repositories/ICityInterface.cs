using city_weather_forecast_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace city_weather_forecast_API.Repositories
{
    public interface ICityInterface<T, K> : IRepository<T, K>
    {
        bool CityExists(K code);
    }
}
