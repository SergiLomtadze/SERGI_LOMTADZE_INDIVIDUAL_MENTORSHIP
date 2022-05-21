using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.BusinessLogic.Validators;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Net.Http;

using System.Threading.Tasks;

namespace ExadelMentorship.BusinessLogic.Features.WeatherFeature
{
    public class CurrentWeatherCommand : ICurrentWeather, ICommand
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public CurrentWeatherCommand(IHttpClientFactory httpClientFactory)
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

    }
}
