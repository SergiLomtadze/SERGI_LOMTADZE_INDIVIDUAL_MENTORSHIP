﻿using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
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
            var weather = DI.Resolve<Weather>();

            //Act 
            var result = await weather.GetTemperatureByCityName("Tbilisi");

            //Assert
            Assert.InRange(result, -100, 100);
        }

        [Fact]
        public async Task GetTemperatureByCityName_WhenCityNameIsNotCorrect_ThrowsException()
        {
            //Arrange
            var weather = DI.Resolve<Weather>();

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => weather.GetTemperatureByCityName("AAA"));
        }
    }
}
