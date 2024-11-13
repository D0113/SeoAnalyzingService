using Microsoft.Extensions.Logging;
using SeoAnalyzing.Common.Constants;
using SeoAnalyzing.Infrastructure.Interfaces.Clients;
using SeoAnalyzing.Infrastructure.Utilities;
using SeoAnalyzing.Model.Search;

namespace SeoAnalyzing.Infrastructure.Core.Clients
{
    public class GoogleSearchClient : IGoogleSearchClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GoogleSearchClient> _logger;

        public GoogleSearchClient(IHttpClientFactory httpClientFactory, ILogger<GoogleSearchClient> logger)
        {
            _httpClient = httpClientFactory.CreateClient(CommonConstants.GoogleClient);
            _logger = logger;
        }

        public async Task<SearchResultModel> SearchAsync(SearchRequestModel searchModel)
        {
            var htmlString = await SearchGoogleAsync(searchModel.SearchQuery, searchModel.SearchLimit);
            var positions = HtmlStringHelper.GetPositions(htmlString, searchModel.SearchUrl, CommonConstants.GoogleUrlPattern, searchModel.SearchLimit);
            var totalCount = positions.Count();

            return new SearchResultModel
            {
                TotalCount = totalCount,
                Positions = totalCount > 0 ? string.Join(", ", positions) : "0"
            };
        }

        private async Task<string> SearchGoogleAsync(string query, int limit)
        {
            var searchUrl = $"search?q={Uri.EscapeDataString(query)}&num={limit}";
            var result = string.Empty;

            try
            {
                var response = await _httpClient.GetAsync(searchUrl);
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("[GoogleSearchClient] - Request error: {message}", ex.Message);
                throw;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError("[GoogleSearchClient] - Request timed out: {message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("[GoogleSearchClient] - Request timed out: {message}", ex.Message);
                throw;
            }

            return result;
        }
    }
}
