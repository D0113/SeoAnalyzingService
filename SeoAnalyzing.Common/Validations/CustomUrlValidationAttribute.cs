using SeoAnalyzing.Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SeoAnalyzing.Common.Validations
{
    public class CustomUrlValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var url = value.ToString();
            var regex = new Regex(ValidationConstants.RegexUrlValidationFormat, RegexOptions.IgnoreCase);

            if (!regex.IsMatch(url)) 
            { 
                return new ValidationResult(ValidationConstants.InvalidTypeErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
