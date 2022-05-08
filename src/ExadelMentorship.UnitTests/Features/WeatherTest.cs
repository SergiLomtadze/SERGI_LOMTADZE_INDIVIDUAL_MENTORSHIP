using ExadelMentorship.BusinessLogic.Exceptions;
using ExadelMentorship.BusinessLogic.Features.WeatherFeature;
using ExadelMentorship.BusinessLogic.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExadelMentorship.UnitTests.Features
{
    [TestClass]
    public class WeatherTest
    {
        [TestMethod]
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
           Assert.IsNotNull(result);
           Assert.IsTrue(result.Temperature.HasValue);                       
        }

        [TestMethod]
        public async Task GetTemperatureByCityName_WhenModelIsNotValid_ReturnsCity()
        {
            //Arrange
            City city = new City()
            {
                Name = "TTT"
            };
            Weather weather = new Weather();

            //Act Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(() => 
                weather.GetTemperatureByCityName(city));            
        }
    }
}
