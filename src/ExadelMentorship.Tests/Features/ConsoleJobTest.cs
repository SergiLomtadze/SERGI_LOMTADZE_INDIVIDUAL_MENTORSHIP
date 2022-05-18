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
        public void GetCityFromInpute_WhenCityNameIsCorrect_ReturnsCorrectCity()
        {
            //Arrange
            var rwMock = new Mock<IRWOperation>();
            rwMock.Setup(p => p.ReadLine()).Returns("Tbilisi");

            MainJob job = new MainJob(rwMock.Object);

            City city = new City()
            {
                Name = "Tbilisi"
            };

            //Act
            var result = job.GetCityFromInput();

            //Assert
            Assert.Equal(city.Name, result.Name);
        }

    }
}
