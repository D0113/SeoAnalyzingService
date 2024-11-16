using SeoAnalyzing.Common.Constants;
using SeoAnalyzing.Common.Validations;
using System.ComponentModel.DataAnnotations;

namespace SeoAnalyzing.UnitTest.Validations
{
    public class DivisibleByTenAttributeTests
    {
        [Fact]
        public void DivisibleByTen_ReturnsSuccess_ForValueDivisibleByTen()
        {
            // Arrange
            var attribute = new DivisibleByTenAttribute(100);
            var context = new ValidationContext(new object());
            var validValue = 50;

            // Act
            var result = attribute.GetValidationResult(validValue, context);

            // Assert
            Assert.Equal(ValidationResult.Success, result);
        }

        [Fact]
        public void DivisibleByTen_ReturnsError_ForValueNotDivisibleByTen()
        {
            // Arrange
            var attribute = new DivisibleByTenAttribute(100);
            var context = new ValidationContext(new object());
            var invalidValue = 55;

            // Act
            var result = attribute.GetValidationResult(invalidValue, context);

            // Assert
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.Equal(ValidationConstants.DivisibleByTenErrorMessage(100), result.ErrorMessage);
        }

        [Fact]
        public void DivisibleByTen_ReturnsError_ForValueGreaterThanMaxValue()
        {
            // Arrange
            var attribute = new DivisibleByTenAttribute(100);
            var context = new ValidationContext(new object());
            var invalidValue = 110;

            // Act
            var result = attribute.GetValidationResult(invalidValue, context);

            // Assert
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.Equal(ValidationConstants.DivisibleByTenErrorMessage(100), result.ErrorMessage);
        }

        [Fact]
        public void DivisibleByTen_ReturnsError_ForValueLessThanOne()
        {
            // Arrange
            var attribute = new DivisibleByTenAttribute(100);
            var context = new ValidationContext(new object());
            var invalidValue = 0;

            // Act
            var result = attribute.GetValidationResult(invalidValue, context);

            // Assert
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.Equal(ValidationConstants.GreaterThanZeroErrorMessage, result.ErrorMessage);
        } 

        [Fact]
        public void DivisibleByTen_ReturnsError_ForInvalidType()
        {
            // Arrange
            var attribute = new DivisibleByTenAttribute(100);
            var context = new ValidationContext(new object());
            var invalidValue = "invalid";

            // Act
            var result = attribute.GetValidationResult(invalidValue, context);

            // Assert
            Assert.NotEqual(ValidationResult.Success, result);
            Assert.Equal(ValidationConstants.InvalidTypeErrorMessage, result.ErrorMessage);
        }
    }
}
