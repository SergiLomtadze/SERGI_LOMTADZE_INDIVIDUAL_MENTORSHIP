﻿using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public class WeatherApiService : IWeatherApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public WeatherApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<double> GetTemperatureByCityName(string name)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={name}&appid=7e66067382ed6a093c3e4b6c22940505&units=metric";
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage result = await httpClient.GetAsync(url);

            if ((int)result.StatusCode == 404)
            {
                throw new NotFoundException($"City: {name} was not found");
            }
            else if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsStringAsync().Result;
                JObject obj = JsonConvert.DeserializeObject<JObject>(json);
                JObject mainObj = obj["main"] as JObject;
                return (double)mainObj["temp"];
            }
            else
            {
                throw new NotFoundException($"Error: {(int)result.StatusCode}");
            }
        }
        public async Task<Coordinate> GetCoordinateByCityName(string name)
        {
            await WeatherHelper.CityNameExistenceValidation(name, _httpClientFactory);

            var url = $"http://api.openweathermap.org/geo/1.0/direct?q={name}&limit=1&appid=7e66067382ed6a093c3e4b6c22940505";
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage result = await httpClient.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsStringAsync().Result;
                JObject[] obj = JsonConvert.DeserializeObject<JObject[]>(json);
                if (obj.Length > 0)
                {
                    return new Coordinate
                    {
                        Latitude = (double)obj[0]["lat"],
                        Longitude = (double)obj[0]["lon"]
                    };
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
        public async Task<List<City>> GetFutureTemperatureByCoordinateAndDayQuantity(Coordinate coordinate, int day)
        {
            var url = $"https://api.openweathermap.org/data/2.5/onecall?lat={coordinate.Latitude}&lon={coordinate.Longitude}&exclude=current,minutely,hourly,alerts&appid=7e66067382ed6a093c3e4b6c22940505&units=metric";
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage result = await httpClient.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var cityList = new List<City>();
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
                        Temperature = temp,
                        Comment = WeatherHelper.GetCommentByTemperature(temp),
                        Date = start
                    });
                }
                return cityList;
            }
            else
            {
                throw new NotFoundException($"Error: {(int)result.StatusCode}");
            }
        }
        public async Task<MaxTempCityInfo> GetTemperatureByCityNameForMaxTemp(string name)
        {
            var watch = new Stopwatch();
            watch.Start();
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={name}&appid=7e66067382ed6a093c3e4b6c22940505&units=metric";
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage result = await httpClient.GetAsync(url);

            if ((int)result.StatusCode == 404)
            {
                return new MaxTempCityInfo
                {
                    Name = name,
                    ErrorMessage = $"City: {name} was not found"
                };
                //throw new NotFoundException($"City: {name} was not found");
            }
            else if (result.IsSuccessStatusCode)
            {
                var json = result.Content.ReadAsStringAsync().Result;
                JObject obj = JsonConvert.DeserializeObject<JObject>(json);
                JObject mainObj = obj["main"] as JObject;
                watch.Stop();
                return new MaxTempCityInfo
                {
                    Name = name,
                    Temperature = (double)mainObj["temp"],
                    DurationTime  = watch.ElapsedMilliseconds
                };
            }
            else
            {
                return new MaxTempCityInfo
                {
                    Name = name,
                    ErrorMessage = $"Error: {(int)result.StatusCode}"
                };
                //3throw new NotFoundException($"Error: {(int)result.StatusCode}");
            }
        }
    }
}
