using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace city_weather_forecast_API.Repositories
{
    public interface IRepository<T, K>
    {
        Task<ICollection<T>> GetAll();
        Task<T> GetById(K id);
        Task<T> Create(T entity);
        Task<bool> Update(K id, T entity);
        Task<bool> Delete(K id);
        Task<bool> DeleteAll();
    }
}
