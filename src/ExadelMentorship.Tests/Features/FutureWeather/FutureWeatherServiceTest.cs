using ExadelMentorship.BusinessLogic.Features.WeatherFeature.FutureWeather;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExadelMentorship.UnitTests.Features.FutureWeather
{
    public class FutureWeatherServiceTest
    {
        [Fact]
        public async Task GetCoordinateByCityName_WhenCityNameIsCorrect_ReturnsCoordinate()
        {
            //Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(p => p.CreateClient(Options.DefaultName)).Returns(HttpMock.FakeHttpClient("[{'lat':41.6934591,lon:44.8014495}]"));
            var coordinate = new Coordinate
            {
                Latitude = 41.6934591,
                Longitude = 44.8014495,
            };
            //Act
            FutureWeatherService futureWeather = new FutureWeatherService(httpClientFactoryMock.Object);
            var result = await futureWeather.GetCoordinateByCityName(It.IsAny<string>());


            //Assert
            Assert.Equal(coordinate.Longitude, result.Longitude);
            Assert.Equal(coordinate.Latitude, result.Latitude);
        }


        [Fact]
        public async Task GetFutureTemperatureByCoordinateAndDayQuantity_WhenCoordinateIsCorrect_ReturnsCityList()
        {
            //Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(p => p.CreateClient(Options.DefaultName))
                .Returns(HttpMock.FakeHttpClient("{'daily':[{'dt':1653379200,'temp':{'day':10 }}]}"));

            var coordinate = new Coordinate
            {
                Latitude = 41.6934591,
                Longitude = 44.8014495,
            };

            var cityList = new List<City>();
            cityList.Add(new City
            {
                Temperature = 10,
                Comment = "It's fresh",
            }) ;

            //Act
            FutureWeatherService futureWeather = new FutureWeatherService(httpClientFactoryMock.Object);
            var result = await futureWeather.GetFutureTemperatureByCoordinateAndDayQuantity(coordinate, 1);


            //Assert
            Assert.Equal(cityList[0].Temperature, result[0].Temperature);
            Assert.Equal(cityList[0].Comment, result[0].Comment);
        }

    }
}
