using System;
using System.Collections.Generic;
using System.Text;
using ExadelMentorship.BusinessLogic.Models;
using FluentValidation;

namespace ExadelMentorship.BusinessLogic.Validators
{
    public class CityValidator : AbstractValidator <City>
    {
        public CityValidator()
        {
            RuleFor(city => city.Name).NotEmpty();
        }
    }
}
