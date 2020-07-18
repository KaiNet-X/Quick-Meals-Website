using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuickMeals.Models.CustomValidation
{
    public class MinValAttribute : ValidationAttribute
    {
        private int MinInt;
        public MinValAttribute(int MinVal)
        {
            MinInt = MinVal;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsNumber(value))
            {
                if ((int)value < MinInt)
                    return new ValidationResult(ErrorMessage != null ? ErrorMessage : $"{value} is below minimum");
            }
            else
            {
                return new ValidationResult("value is not a number");
            }
            return ValidationResult.Success;
        }

        private bool IsNumber(object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }
    }
}
