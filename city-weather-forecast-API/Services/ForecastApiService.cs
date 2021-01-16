using city_weather_forecast_API.Models;
using city_weather_forecast_API.Services.ServiceInterfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace city_weather_forecast_API.Services
{
    public enum TemperatureExtremum
    {
        Min,
        Max
    }

    delegate bool CompareTemperatureDelegate(double temperature, double forecast);

    public class ForecastApiService : IForecastApi
    {
        static readonly HttpClient client = new HttpClient();

        private readonly string GetPlaceForecastUrlPath = "https://api.meteo.lt/v1/places/{0}/forecasts/long-term";
        private readonly string GetAllPlacesUrlPath = "https://api.meteo.lt/v1/places";

        public async Task<CitySelection[]> GetAllPlaces()
        {
            HttpResponseMessage response = await client.GetAsync(GetAllPlacesUrlPath);
            List<CitySelection> cityList = new List<CitySelection>();

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                JArray jsonResponseArray = JArray.Parse(responseString);

                foreach (JToken place in jsonResponseArray)
                {
                    cityList.Add(
                        new CitySelection
                        {
                            Name = place.Value<string>("name"),
                            PlaceCode = place.Value<string>("code")
                        }
                    );
                }

                return cityList.ToArray();
            }
            else
            {
                return null;
            }
        }

        public async Task<string> GetTemperature(string CityCode, TemperatureExtremum extremumType)
        {
            string fullApiUrlPath = string.Format(GetPlaceForecastUrlPath, CityCode);
            HttpResponseMessage response = await client.GetAsync(fullApiUrlPath);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                JObject jsonResponseObject = JObject.Parse(responseString);

                switch (extremumType)
                {
                    case TemperatureExtremum.Max:
                        return GetTemperatureFromJson(jsonResponseObject, -100, LessThenForecast).ToString();
                    case TemperatureExtremum.Min:
                        return GetTemperatureFromJson(jsonResponseObject, 100, MoreThenForecast).ToString();
                    default:
                        break;
                }
            }

            return "-";
        }

        static double GetTemperatureFromJson(JObject jsonObject, double initTemperature, CompareTemperatureDelegate comperison)
        {
            double temperature = initTemperature;
            foreach (JToken forecast in jsonObject["forecastTimestamps"])
            {
                string dateString = forecast.Value<string>("forecastTimeUtc");
                double airTemperature = forecast.Value<double>("airTemperature");

                if (DateTime.TryParse(dateString, out DateTime forecastTime))
                {
                    if (IsStillNextDay(forecastTime) && comperison(temperature, airTemperature))
                    {
                        temperature = airTemperature;
                    }
                }
            }

            return temperature;
        }

        static bool IsStillNextDay(DateTime forecastDate)
        {
            DateTime endTime = DateTime.Now.AddHours(24);
            if (DateTime.Now <= forecastDate && endTime >= forecastDate)
                return true;
            else
                return false;
        }

        readonly CompareTemperatureDelegate MoreThenForecast = (temperature, forecast) => temperature > forecast;
        readonly CompareTemperatureDelegate LessThenForecast = (temperature, forecast) => temperature < forecast;
    }
}
