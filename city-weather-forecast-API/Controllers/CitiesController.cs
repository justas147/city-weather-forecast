using city_weather_forecast_API.Models;
using city_weather_forecast_API.Models.DTO;
using city_weather_forecast_API.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace city_weather_forecast_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _citiesService;
        private readonly IForecastApi _forecastApiService;

        public CitiesController(ICityService citiesService, IForecastApi forecastApiService)
        {
            _citiesService = citiesService;
            _forecastApiService = forecastApiService;
        }

        // GET: api/Cities
        [HttpGet]
        [Produces(typeof(CityDetailsDTO[]))]
        public async Task<IActionResult> GetCityItem()
        {
            var cities = await _citiesService.GetAll();

            return Ok(cities);
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        [Produces(typeof(CityDetailsDTO))]
        public async Task<IActionResult> GetCity(string id)
        {
            var city = await _citiesService.GetById(id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // PUT: api/Cities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(string id, City city)
        {
            await _citiesService.Update(id, city);

            return NoContent();
        }

        // POST: api/Cities
        [HttpPost]
        [Produces(typeof(CityDetailsDTO))]
        public async Task<IActionResult> PostCity(City city)
        {
            await _citiesService.Create(city);

            return CreatedAtAction("GetCity", new { id = city.PlaceCode }, city);
        }

        // DELETE: api/Cities/
        [HttpDelete()]
        public async Task<IActionResult> DeleteAll()
        {
            await _citiesService.DeleteAll();

            return NoContent();
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(string id)
        {
            await _citiesService.Delete(id);

            return NoContent();
        }

        // GET: api/Cities/selection
        [HttpGet("selection")]
        [Produces(typeof(CitySelection[]))]
        public async Task<IActionResult> GetPlaces()
        {
            var places = await _forecastApiService.GetAllPlaces();

            return Ok(places);
        }
    }
}
