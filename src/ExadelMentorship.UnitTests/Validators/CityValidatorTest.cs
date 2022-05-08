using ExadelMentorship.BusinessLogic.Models;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator = ExadelMentorship.BusinessLogic.Validators.CityValidator;

namespace ExadelMentorship.UnitTests.Validators
{
    [TestClass]
    public class CityValidatorTest
    {
        public readonly Validator _validator;
        public CityValidatorTest()
        {
            _validator = new Validator();
        }

        [TestMethod]
        public void RuleForCityName_WhenCityNameIsEmpty_ShouldHaveValidationError()
        {
            var model = new City
            {
                Name = String.Empty,
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [TestMethod]
        public void RuleForCityName_WhenCityNameIsNotEmpty_ShouldNotHaveValidationError()
        {
            var model = new City
            {
                Name = "Kutaisi",
            };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
    }
}
