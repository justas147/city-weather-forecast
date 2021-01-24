using city_weather_forecast_API.IntegrationTests.Ordering;
using city_weather_forecast_API.Models;
using city_weather_forecast_API.Models.DTO;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace city_weather_forecast_API.IntegrationTests
{
    [TestCaseOrderer("city-weather-forecast-API.IntegrationTests.Ordering.PriorityOrderer", 
        "city-weather-forecast-API.IntegrationTests")]
    public class EndpointTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper output;

        public EndpointTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
        {
            _factory = factory;
            this.output = output;
        }

        [Fact, TestPriority(0)]
        public async Task GetAll_GetAllAddedCities_ReturnsSelectedCityList()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Cities");
            CityDetailsDTO[] cities = JsonConvert.DeserializeObject<CityDetailsDTO[]>(
                await response.Content.ReadAsStringAsync());

            output.WriteLine(await response.Content.ReadAsStringAsync());
            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(cities);
            Assert.Empty(cities);
        }

        [Fact, TestPriority(1)]
        public async Task GetPlaces_GetAllSelectionCities_ReturnsCityList()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Cities/selection");
            CityDetailsDTO[] cities = JsonConvert.DeserializeObject<CityDetailsDTO[]>(
                await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(cities);
            Assert.NotEmpty(cities);
        }

        [Fact, TestPriority(2)]
        public async Task Get_GetSpecificCity_ReturnsCityByPlaceCode()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/Cities/kaunas");
            CityDetailsDTO city = JsonConvert.DeserializeObject<CityDetailsDTO>(
                await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotNull(city);
            Assert.Equal("Kaunas", city.Name);
        }

        [Fact, TestPriority(3)]
        public async Task Post_PostSpecificCity_ReturnsAddedCity()
        {
            // Arrange
            var client = _factory.CreateClient();
            City city = new City
            {
                PlaceCode = "baibiai",
                Name = "Baibiai",
                Description = "New city"
            };

            // Act
            var response = await client.PostAsync("/api/Cities/", 
                new StringContent(JsonConvert.SerializeObject(city), Encoding.UTF8, "application/json"));
            CityDetailsDTO addedCity = JsonConvert.DeserializeObject<CityDetailsDTO>(
                await response.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(city);
            Assert.Equal("baibiai", addedCity.PlaceCode);
        }

        [Fact, TestPriority(4)]
        public async Task Put_EditSpecificCity_ReturnsEditedCity()
        {
            // Arrange
            var client = _factory.CreateClient();
            City city = new City
            {
                PlaceCode = "kaunas",
                Name = "Kaunas",
                Description = "The description is edited"
            };

            // Act
            var response = await client.PutAsync("/api/Cities/kaunas", 
                new StringContent(JsonConvert.SerializeObject(city), Encoding.UTF8, "application/json"));

            var getCity = await client.GetAsync("/api/Cities/kaunas");
            CityDetailsDTO editedCity = JsonConvert.DeserializeObject<CityDetailsDTO>(
                await getCity.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode();
            getCity.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.NotNull(editedCity);
            Assert.Equal("The description is edited", editedCity.Description);
        }

        [Fact, TestPriority(5)]
        public async Task Delete_DeleteSpecificCity_CityIsDeleted()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/Cities/vilnius");

            var getCity = await client.GetAsync("/api/Cities/vilnius");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, getCity.StatusCode);
        }

        [Fact, TestPriority(6)]
        public async Task DeleteAll_DeleteAllCities_CitiesAreDeleted()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.DeleteAsync("/api/Cities/");

            var getCities = await client.GetAsync("/api/Cities/");
            CityDetailsDTO[] cities = JsonConvert.DeserializeObject<CityDetailsDTO[]>(
                await getCities.Content.ReadAsStringAsync());

            // Assert
            response.EnsureSuccessStatusCode();
            getCities.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Empty(cities);
        }
    }
}
