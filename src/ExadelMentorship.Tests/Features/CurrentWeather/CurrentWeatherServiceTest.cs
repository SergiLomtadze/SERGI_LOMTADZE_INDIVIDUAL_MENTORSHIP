
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ExadelMentorship.UnitTests.Features
{
    public class CurrentWeatherServiceTest 
    {
        [Fact]
        public void GetCommentByTemperature_WhenTemperatureLessThenZero_ReturnsDressWarmly()
        {
            //Arrange

            //Act
            var result = WeatherHelper.GetCommentByTemperature(-5);

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
            CurrentWeatherService currentWeather = new CurrentWeatherService(httpClientFactoryMock.Object);
            var result = await currentWeather.GetTemperatureByCityName(It.IsAny<string>());

            //Assert
            Assert.Equal(30.0, result);
        }



    }
}
