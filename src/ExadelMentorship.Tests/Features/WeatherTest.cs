using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Models;
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
            Weather weather = new Weather(FakeHttpClient("{'main':{'temp':30.0}}"));

            //Act
            var result = await weather.GetTemperatureByCityName(It.IsAny<string>());

            //Assert
            Assert.Equal(30.0, result);
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
