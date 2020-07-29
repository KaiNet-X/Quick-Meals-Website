using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Sujith_Site.Models.CustomAttributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        //uses array of strings in constructor for valid extentions
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }
        //if using an IFormFile, check if the extension is one of the allowed extentions
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
            var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"This file extension is not allowed!";
        }
    }
}
