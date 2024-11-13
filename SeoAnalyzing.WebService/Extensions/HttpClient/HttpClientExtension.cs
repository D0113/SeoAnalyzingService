using SeoAnalyzing.Common.Constants;

namespace SeoAnalyzing.WebService.Extensions.HttpClient
{
    public static class HttpClientExtension
    {
        public static void RegisterHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient(CommonConstants.GoogleClient, client =>
            {
                client.BaseAddress = new Uri(CommonConstants.GoogleUrl);
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");
            });
            services.AddHttpClient(CommonConstants.BingClient, client =>
            {
                client.BaseAddress = new Uri(CommonConstants.BingUrl);
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");
            });
        }
    }
}
