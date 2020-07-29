using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sujith_Site.Models.CustomAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        private readonly FileSizeUnit unit;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }
        public MaxFileSizeAttribute(int maxFileSize, FileSizeUnit unit)
        {
            _maxFileSize = maxFileSize;
            this.unit = unit;
        }
        //checks if file is below a max size
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = (IFormFile)value;
            if (file != null)
            {
                if (file.Length > _maxFileSize * (Math.Pow( 1024,(int)unit)))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is { _maxFileSize / Math.Pow(1024, (int)unit)} {unit}";
        }

        //reasonable file size units for image upload
        public enum FileSizeUnit
        {
            B,
            KB,
            MB
        }
    }
}
