using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public class FutureWeatherCommand : IFutureWeather, ICommand
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public List<City> cityList; 
        private Coordinate coordinate;
        private string cityName;
        public FutureWeatherCommand(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            cityList = new List<City>();    
            coordinate = new Coordinate();
        }

        public List<City> GetCityList()
        {
            return cityList;
        }
        public async Task GetCoordinateByCityName(string name)
        {         
            cityName = name;
            await CityNameValidation(cityName);

            var url = $"http://api.openweathermap.org/geo/1.0/direct?q={name}&limit=1&appid=7e66067382ed6a093c3e4b6c22940505";
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage result = await httpClient.GetAsync(url);

            if ((int)result.StatusCode == 404)
            {
                throw new NotFoundException($"City: {name} was not found");
            }
            else if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsStringAsync().Result;
                JObject []obj = JsonConvert.DeserializeObject < JObject[]>(json);
                if (obj.Length>0)
                {
                    coordinate.Latitude = (double)obj[0]["lat"];
                    coordinate.Longitude = (double)obj[0]["lon"];
                }
                else
                {
                    throw new NotFoundException("Incorrect Name");
                }

            }
            else
            {
                throw new NotFoundException($"Error: {(int)result.StatusCode}");
            }

        }
        public async Task GetFutureTemperatureByCoordinate(int day)
        {            
            var url = $"https://api.openweathermap.org/data/2.5/onecall?lat={coordinate.Latitude}&lon={coordinate.Longitude}&exclude=current,minutely,hourly,alerts&appid=7e66067382ed6a093c3e4b6c22940505&units=metric";
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage result = await httpClient.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsStringAsync().Result;
                JObject obj = JsonConvert.DeserializeObject<JObject>(json);
                for (int i = 0; i < day; i++)
                {
                    JObject daily = obj["daily"][i] as JObject;
                    var timeUnix = (long)daily["dt"];
                    JObject tempObj = daily["temp"] as JObject;
                    var temp = (double)tempObj["day"];

                    DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0); 
                    start = start.AddSeconds(timeUnix).Date; 

                    cityList.Add(new City
                    {
                        Name = cityName,
                        Temperature = temp,
                        Comment = WeatherHelper.GetCommentByTemperature(temp),
                        Date = start
                    });
                }
            }
            else
            {
                throw new NotFoundException($"Error: {(int)result.StatusCode}");
            }
        }

        private async Task CityNameValidation(string name)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={name}&appid=7e66067382ed6a093c3e4b6c22940505&units=metric";
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage result = await httpClient.GetAsync(url);

            if ((int)result.StatusCode == 404)
            {
                throw new NotFoundException($"City: {name} was not found");
            }
        }
    }
    public class Coordinate
    {
        public double Latitude { get; set; }    
        public double Longitude { get; set; }
    }
}
