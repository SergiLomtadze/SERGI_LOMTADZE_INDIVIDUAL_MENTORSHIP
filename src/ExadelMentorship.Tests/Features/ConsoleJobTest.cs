using ExadelMentorship.BusinessLogic;
using ExadelMentorship.BusinessLogic.Features;
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
        public void GetCityFromConsole_WhenCityNameIsCorrect_ReturnsCorrectCity()
        {
            //Arrange
            var consoleMock = new Mock<IRWOperation>();
            consoleMock.Setup(p => p.ReadLine()).Returns("Tbilisi");

            ConsoleJob consoleJob = new ConsoleJob(consoleMock.Object);
            City city = new City()
            {
                Name = "Tbilisi"
            };

            //Act
            var result = consoleJob.GetCityFromConsole();

            //Assert
            Assert.Equal(city.Name, result.Name);
        }
    }
}
