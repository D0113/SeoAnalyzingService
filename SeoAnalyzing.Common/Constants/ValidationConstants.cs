namespace SeoAnalyzing.Common.Constants
{
    public static class ValidationConstants
    {
        public const int SearchLimitDefaultValue = 100;
        public const string RegexUrlValidationFormat = @"^(?:(ftp|http|https)?:\/\/)?(?:[\w-]+\.)+([a-zA-Z0-9]){2,6}$";
        public const string GreaterThanZeroErrorMessage = "Value must be greater than 0."; 
        public const string InvalidTypeErrorMessage = "Invalid input type."; 
        public const string InvalidUrlErrorMessage = $"Invalid URL. The URL should be in a valid domain format and may include a path, query string, and fragment.)"; 
        public static string DivisibleByTenErrorMessage(int limit) => $"Value must be divisible by 10 and less than or equal {limit}.";
        
    }
}
