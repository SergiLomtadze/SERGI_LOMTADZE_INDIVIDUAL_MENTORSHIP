
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
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
    public class WeatherTest 
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
            httpClientFactoryMock.Setup(p => p.CreateClient(Options.DefaultName)).Returns(FakeHttpClient("{'main':{'temp':30.0}}"));

            var rwMock = new Mock<IRWOperation>();


            CurrentWeather weather = new CurrentWeather(httpClientFactoryMock.Object, rwMock.Object);

            //Act
            var result = await weather.GetTemperatureByCityName(It.IsAny<string>());

            //Assert
            Assert.Equal(30.0, result);
        }

        [Fact]
        public async Task Execute_WhenCityNameIsCorrect_ReturnsWeatherInfo()
        {
            //Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(p => p.CreateClient(Options.DefaultName)).Returns(FakeHttpClient("{'main':{'temp':10.0}}"));

            string firstOutput = string.Empty;
            string secondOutput = string.Empty;
            City city = new City();

            var rwMock = new Mock<IRWOperation>();

            rwMock.Setup(p => p.ReadLine()).Returns("Tbilisi");

            rwMock.Setup(p => p.WriteLine("Please enter the city Name:"))
                .Callback<string>(b => firstOutput = b);

            rwMock.Setup(p => p.WriteLine("In Tbilisi temperature is: 10, It's fresh"))
                .Callback<string>(b => secondOutput = b);

            var weatherMock = new Mock<ICurrentWeather>();

            weatherMock.Setup(x => x.ValidateCityName(It.IsAny<City>()))
                .Callback<City>(b => city = b);

            weatherMock.Setup(x => x.GetTemperatureByCityName(It.IsAny<string>())).Returns(Task.FromResult(10.0));


            //Act
            //MainJob job = new MainJob(rwMock.Object, weatherMock.Object);
            //var result = job.Do();

            CurrentWeather weather = new CurrentWeather(httpClientFactoryMock.Object, rwMock.Object);
            await weather.Execute();

            //Assert
            //Assert.Equal("Tbilisi", city.Name);
            Assert.Equal("Please enter the city Name:", firstOutput);
            Assert.Equal("In Tbilisi temperature is: 10, It's fresh", secondOutput);
        }

        public static HttpClient FakeHttpClient(string response) 
        {
            response ??= "{'main':{'temp':10.0}}";
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(response),
               });
            return new HttpClient(handlerMock.Object);  
        }

    }
}
