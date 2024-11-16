using SeoAnalyzing.Common.Constants;
using SeoAnalyzing.Common.Validations;
using System.ComponentModel.DataAnnotations;

namespace SeoAnalyzing.UnitTest.Validations
{
    public class CustomUrlValidationAttributeTests
    {
        [Fact]
        public void CustomUrlValidation_ReturnsSuccess_ForValidUrl()
        {
            // Arrange
            var attribute = new CustomUrlValidationAttribute();
            var context = new ValidationContext(new object());
            var validUrl = "https://example.com";
            // Act
            var result = attribute.GetValidationResult(validUrl, context);

            // Assert
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void CustomUrlValidation_ReturnsError_ForInvalidUrl()
        {
            // Arrange
            var attribute = new CustomUrlValidationAttribute();
            var context = new ValidationContext(new object());
            var invalidUrl = "examp#le.com";

            // Act
            var result = attribute.GetValidationResult(invalidUrl, context);

            // Assert
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.Equal(ValidationConstants.InvalidTypeErrorMessage, result.ErrorMessage);
        }
    }
}
