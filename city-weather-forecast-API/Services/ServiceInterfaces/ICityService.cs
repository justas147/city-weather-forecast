using city_weather_forecast_API.Models;
using city_weather_forecast_API.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace city_weather_forecast_API.Services.ServiceInterfaces
{
    public interface ICityService
    {
        Task<CityDetailsDTO> GetById(string code);
        Task<ICollection<CityDetailsDTO>> GetAll();
        Task<City> Create(City newItem);
        Task Update(string id, City updateData);
        Task<bool> DeleteAll();
        Task<bool> Delete(string code);
    }
}
