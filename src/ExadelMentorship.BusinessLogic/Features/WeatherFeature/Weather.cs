using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.BusinessLogic.Validators;
using FluentValidation.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public class Weather
    {
        public double Temperature { get; set; }

        public void Validate(City city)
        {
            CityValidator validator = new CityValidator();
            ValidationResult results = validator.Validate(city);
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    throw new NotFoundException($"City was not inputed");
                }
            }            
        }
        public async Task<City> GetTemperatureByCityName(City city)
        {
            using (var httpClient = new HttpClient())
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city.Name}&appid=7e66067382ed6a093c3e4b6c22940505&units=metric";

                HttpResponseMessage result = await httpClient.GetAsync(url);
                if ((int)result.StatusCode == 404)
                {
                    throw new NotFoundException($"City: {city.Name} was not found");
                }
                
                if (result.IsSuccessStatusCode)
                {
                    var json = result.Content.ReadAsStringAsync().Result;
                    JObject obj = JsonConvert.DeserializeObject<JObject>(json);
                    JObject mainObj = obj["main"] as JObject;
                    city.Temperature = (double)mainObj["temp"];
                    city.FillComment();
                }                
            }
            return city;   
        }
    }
}
