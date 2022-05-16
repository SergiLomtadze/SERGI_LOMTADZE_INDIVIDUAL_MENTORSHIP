using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExadelMentorship.IntegrationTests.Features
{
    public class WeatherTest
    {

        [Fact]
        public async Task GetTemperatureByCityName_WhenCityNameIsCorrect_ReturnsTemperature()
        {
            //Arrange
            DIConfigurer dIConfigurer = new DIConfigurer();
            Weather weather = new Weather(dIConfigurer.GetHttpClientFactory());

            //Act 
            var result = await weather.GetTemperatureByCityName("Tbilisi");

            //Assert
            Assert.InRange(result, -100, 100);
        }

        [Fact]
        public async Task GetTemperatureByCityName_WhenCityNameIsNotCorrect_ThrowsException()
        {
            //Arrange
            DIConfigurer dIConfigurer = new DIConfigurer();
            Weather weather = new Weather(dIConfigurer.GetHttpClientFactory());

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => weather.GetTemperatureByCityName("AAA"));
        }
    }
}
