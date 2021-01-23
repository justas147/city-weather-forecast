
namespace city_weather_forecast_API.Models.DTO
{
    public class CityDetailsDTO
    {
        public string PlaceCode { get; set; }
        public string Name { get; set; }
        public string MaxTemperature { get; set; }
        public string MinTemperature { get; set; }
        public string Description { get; set; }
    }
}
