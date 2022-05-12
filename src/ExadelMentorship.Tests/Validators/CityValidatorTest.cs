using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator = ExadelMentorship.BusinessLogic.Validators.CityValidator;
using Xunit;
using ExadelMentorship.BusinessLogic.Models;
using FluentValidation.TestHelper;

namespace ExadelMentorship.Tests.Validators
{
    public class CityValidatorTest
    {
        public readonly Validator _validator;
        public CityValidatorTest()
        {
            _validator = new Validator();
        }

        [Fact]
        public void RuleForCityName_WhenCityNameIsEmpty_ShouldHaveValidationError()
        {
            var model = new City
            {
                Name = String.Empty,
            };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
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
