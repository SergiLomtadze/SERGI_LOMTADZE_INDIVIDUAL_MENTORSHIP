using ExadelMentorship.BusinessLogic.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Models
{
    public class City
    {
        public string Name { get; set; }
        public double? Temperature { get; set; } = null;
        public string Comment { get; set; }

        public void FillComment()
        {
            if (this.Temperature != null)
            {
                if (this.Temperature > 30)
                {
                    this.Comment = "It's time to go to the beach";
                }
                else if (this.Temperature > 20)
                {
                    this.Comment = "Good weather";
                }
                else if (this.Temperature > 0)
                {
                    this.Comment = "It's fresh";
                }
                else
                {
                    this.Comment = "Dress warmly";
                }
            }
        }

    }
}
