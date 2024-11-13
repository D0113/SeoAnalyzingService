namespace SeoAnalyzing.Common.Constants
{
    public static class CommonConstants
    {
        public const string GoogleUrl = "https://www.google.com";
        public const string GoogleClient = nameof(GoogleClient);
        public const string BingUrl = "https://www.bing.com";
        public const string BingClient = nameof(BingClient);
        //public const string DefaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";
        public const string GoogleUrlPattern = @"data-id=""atritem-https:\/\/(.*?)\""";
        public const string BingUrlPattern = @"<div\sclass=""tpmeta"">(.*?)<\/cite>";
        public const string SeoAnalyzingCors = nameof(SeoAnalyzingCors);
        public const int LimitItemPerPage = 10;
    }
}

