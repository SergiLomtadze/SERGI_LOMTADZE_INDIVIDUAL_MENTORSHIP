
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Models;
using ExadelMentorship.BusinessLogic.Services;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ExadelMentorship.UnitTests.Features
{
    public class WeatherApiServiceTest 
    {
        [Fact]
        public void GetCommentByTemperature_WhenTemperatureLessThenZero_ReturnsDressWarmly()
        {
            //Arrange

            //Act
            var result = WeatherHelperService.GetCommentByTemperature(-5);

            //Assert
            Assert.Equal("Dress warmly",result);
        }

        [Fact]
        public async Task GetTemperatureByCityName_WhenCityNameIsCorrect_ReturnsTemperature()
        {
            //Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(p => p.CreateClient(Options.DefaultName)).Returns(HttpMock.FakeHttpClient("{'main':{'temp':30.0}}"));

            //Act
            WeatherApiService currentWeather = new WeatherApiService(httpClientFactoryMock.Object);
            var result = await currentWeather.GetTemperatureByCityName(It.IsAny<string>());

            //Assert
            Assert.Equal(30.0, result);
        }

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
            WeatherApiService weatherApiService = new WeatherApiService(httpClientFactoryMock.Object);
            var result = await weatherApiService.GetCoordinateByCityName(It.IsAny<string>());


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
                Name = "Tbilisi"
            });

            //Act
            WeatherApiService weatherApiService = new WeatherApiService(httpClientFactoryMock.Object);
            var result = (await weatherApiService.GetFutureTemperatureByCoordinateAndDayQuantity(coordinate, 1, "Tbilisi")).ToList();

            //Assert
            Assert.Equal(cityList[0].Temperature, result[0].Temperature);
            Assert.Equal(cityList[0].Comment, result[0].Comment);
        }



    }
}
