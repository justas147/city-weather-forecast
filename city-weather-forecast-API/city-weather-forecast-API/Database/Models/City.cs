using System.ComponentModel.DataAnnotations;

namespace city_weather_forecast_API.Models
{
    public class City
    {
        [Key]
        [Required]
        public string PlaceCode { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}
