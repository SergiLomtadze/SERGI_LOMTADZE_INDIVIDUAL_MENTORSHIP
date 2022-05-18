using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features;
using ExadelMentorship.BusinessLogic.Interfaces;
using ExadelMentorship.BusinessLogic.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExadelMentorship.UnitTests.Features
{
    public class ConsoleJobTest
    {
        [Fact]
        public void Execute_WhenCityNameIsCorrect_ReturnsWeatherInfo()
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

            var weatherMock = new Mock<IWeather>();

            weatherMock.Setup(x => x.ValidateCityName(It.IsAny<City>()))
                .Callback<City>(b => city = b);

            weatherMock.Setup(x => x.GetTemperatureByCityName(It.IsAny<string>())).Returns(Task.FromResult(10.0));

            //Act
            MainJob job = new MainJob(rwMock.Object, weatherMock.Object);
            var result = job.Execute();

            //Assert
            Assert.Equal("Tbilisi", city.Name);
            Assert.Equal("Please enter the city Name:", firstOutput);
            Assert.Equal("In Tbilisi temperature is: 10, It's fresh", secondOutput);
        }

    }
}
