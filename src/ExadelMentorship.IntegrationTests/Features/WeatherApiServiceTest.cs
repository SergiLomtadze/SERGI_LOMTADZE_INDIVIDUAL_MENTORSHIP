using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using System.Threading.Tasks;
using Xunit;

namespace ExadelMentorship.IntegrationTests.Features
{
    public class WeatherApiServiceTest
    {

        [Fact]
        public async Task GetTemperatureByCityName_WhenCityNameIsCorrect_ReturnsTemperature()
        {
            //Arrange
            var weather = DI.Resolve<IWeatherApiService>();

            //Act 
            var result = await weather.GetTemperatureByCityName("Tbilisi");

            //Assert
            Assert.InRange(result, -100, 100);
        }

        [Fact]
        public async Task GetTemperatureByCityName_WhenCityNameIsNotCorrect_ThrowsException()
        {
            //Arrange
            var weather = DI.Resolve<IWeatherApiService>();

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => weather.GetTemperatureByCityName("AAA"));
        }

        [Fact]
        public async Task GetCoordinateByCityName_WhenCityNameIsCorrect_ReturnsCoordinate()
        {
            //Arrange
            var weather = DI.Resolve<IWeatherApiService>();

            //Act 
            var result = await weather.GetCoordinateByCityName("Tbilisi");

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetFutureTemperatureByCoordinateAndDayQuantity_WhenCityNameIsCorrect_ReturnsCoordinate()
        {
            //Arrange
            var weather = DI.Resolve<IWeatherApiService>();
            var coordinate = new Coordinate
            {
                Latitude = 41.6934591,
                Longitude = 44.8014495,
            };

            //Act 
            var result = await weather.GetFutureTemperatureByCoordinateAndDayQuantity(coordinate, 1);

            //Assert
            Assert.NotNull(result);
        }
    }
}
