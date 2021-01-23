using city_weather_forecast_API.Database;
using city_weather_forecast_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace city_weather_forecast_API.Repositories
{
    public class CityRepository : IRepository<City, string>
    {
        private readonly CityDbContext _context;

        public CityRepository(CityDbContext context)
        {
            _context = context;
        }

        public async Task<City> Create(City entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(string code)
        {
            var city = await _context.CityItem.FindAsync(code);
            if (city == null)
            {
                return true;
            }

            _context.Entry(city).State = EntityState.Deleted;
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> DeleteAll()
        {
            var cities = await _context.CityItem.ToListAsync();
            _context.CityItem.RemoveRange(cities);
            await _context.SaveChangesAsync();
            return !_context.CityItem.Any();
        }

        public async Task<ICollection<City>> GetAll()
        {
            var cities = await _context.CityItem.ToArrayAsync();
            return cities;
        }

        public async Task<City> GetById(string id)
        {
            var city = await _context.CityItem.FindAsync(id);
            return city;
        }

        public async Task<bool> Update(string id, City entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public bool DoesEntityExist(string id)
        {
            return _context.CityItem.Any(e => e.PlaceCode == id);
        }
    }
}
