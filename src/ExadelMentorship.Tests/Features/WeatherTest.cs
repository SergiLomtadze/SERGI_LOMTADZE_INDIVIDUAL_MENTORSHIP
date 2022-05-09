using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Models;
using System.Threading.Tasks;
using Xunit;

namespace ExadelMentorship.Tests.Features
{
    public class WeatherTest
    {
        [Fact]
        public async Task GetTemperatureByCityName_WhenModelIsValid_ReturnsCity()
        {
            //Arrange
            City city = new City()
            {
                Name = "Tbilisi"
            };
            Weather weather = new Weather();

            //Act
            var result = await weather.GetTemperatureByCityName(city);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Temperature.HasValue);
        }

        [Fact]
        public async Task GetTemperatureByCityName_WhenModelIsNotValid_ReturnsCity()
        {
            //Arrange
            City city = new City()
            {
                Name = "TTT"
            };
            Weather weather = new Weather();

            //Act Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                weather.GetTemperatureByCityName(city));
        }
    }
}
