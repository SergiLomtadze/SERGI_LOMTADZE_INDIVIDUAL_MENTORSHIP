using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ExadelMentorship.UnitTests.Features
{
    public class CurrentWeatherCommandHandlerTest
    {
        [Fact]
        public void Handle_WhenCityNameIsCorrect_ReturnsWeatherInfo()
        {
            //Arrange
            string firstOutput = string.Empty;
            string secondOutput = string.Empty;
            City city = new City();

            var rwMock = new Mock<IRWOperation>();

            rwMock.Setup(p => p.ReadLine()).Returns("Tbilisi");

            rwMock.Setup(p => p.WriteLine("Please enter the city Name:"))
                .Callback<string>(b => firstOutput = b);

            rwMock.Setup(p => p.WriteLine("In Tbilisi temperature is: 10, It's fresh"))
                .Callback<string>(b => secondOutput = b);

            var currentWeatherServiceMock = new Mock<ICurrentWeatherService>();

            currentWeatherServiceMock.Setup(x => x.GetTemperatureByCityName(It.IsAny<string>())).Returns(Task.FromResult(10.0));

            //Act
            CurrentWeatherCommandHandler obj = new CurrentWeatherCommandHandler(rwMock.Object, currentWeatherServiceMock.Object);
            var result = obj.Handle(new CurrentWeatherCommand());

            //Assert
            Assert.Equal("Please enter the city Name:", firstOutput);
            Assert.Equal("In Tbilisi temperature is: 10, It's fresh", secondOutput);
        }

    }
}
