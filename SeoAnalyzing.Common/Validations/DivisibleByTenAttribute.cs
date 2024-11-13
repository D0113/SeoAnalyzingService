using SeoAnalyzing.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace SeoAnalyzing.Common.Validations
{
    public class DivisibleByTenAttribute : ValidationAttribute
    {
        private readonly int _maxValue;

        public DivisibleByTenAttribute(int MaxValue)
        {
            _maxValue = MaxValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int intValue)
            {
                if (intValue < 1)
                {
                    return new ValidationResult(ValidationConstants.GreaterThanZeroErrorMessage);
                }

                if (intValue % 10 == 0 && intValue <= _maxValue)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult(ValidationConstants.DivisibleByTenErrorMessage(_maxValue));
            }

            return new ValidationResult(ValidationConstants.InvalidTypeErrorMessage);
        }
    }
}
