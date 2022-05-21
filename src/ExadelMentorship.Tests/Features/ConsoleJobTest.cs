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
        public void CurrentWeatherExecutor_WhenCityNameIsCorrect_ReturnsWeatherInfo()
        {
            //Arrange
            string firstOutput = string.Empty;
            string secondOutput = string.Empty;

            var rwMock = new Mock<IRWOperation>();

            rwMock.Setup(p => p.ReadLine()).Returns("Tbilisi");

            rwMock.Setup(p => p.WriteLine("Please enter the city Name:"))
                .Callback<string>(b => firstOutput = b);

            rwMock.Setup(p => p.WriteLine("In Tbilisi temperature is: 10, It's fresh"))
                .Callback<string>(b => secondOutput = b);

            var weatherMock = new Mock<ICurrentWeather>();

            weatherMock.Setup(x => x.GetTemperatureByCityName(It.IsAny<string>())).Returns(Task.FromResult(10.0));

            //Act
            CommandInvoker job = new CommandInvoker(rwMock.Object, weatherMock.Object);
            var result = job.CurrentWeatherExecutor();

            //Assert
            Assert.Equal("Please enter the city Name:", firstOutput);
            Assert.Equal("In Tbilisi temperature is: 10, It's fresh", secondOutput);
        }

    }
}
