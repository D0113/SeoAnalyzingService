using Microsoft.Extensions.Logging;
using SeoAnalyzing.Common.Constants;
using SeoAnalyzing.Infrastructure.Interfaces.Clients;
using SeoAnalyzing.Infrastructure.Utilities;
using SeoAnalyzing.Model.Search;
using System.Text;

namespace SeoAnalyzing.Infrastructure.Core.Clients
{
    public class BingSearchClient : IBingSearchClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BingSearchClient> _logger;

        public BingSearchClient(IHttpClientFactory httpClientFactory, ILogger<BingSearchClient> logger)
        {
            _httpClient = httpClientFactory.CreateClient(CommonConstants.BingClient);
            _logger = logger;
        }

        public async Task<SearchResultModel> SearchAsync(SearchRequestModel searchModel)
        {
            var totalPages = searchModel.SearchLimit > CommonConstants.LimitItemPerPage ?
                             searchModel.SearchLimit / CommonConstants.LimitItemPerPage : 1;
            var searchBingTask = new List<Task<string>>();
            StringBuilder a = new StringBuilder();

            for (int page = 0; page < totalPages; page++)
            {
                searchBingTask.Add(SearchBingAsync(searchModel.SearchQuery, page));
            }

            var searchBingTaskResult = await Task.WhenAll(searchBingTask);
            var htmlString = string.Join(string.Empty, searchBingTaskResult);
            var positions = HtmlStringHelper.GetPositions(htmlString, searchModel.SearchUrl, CommonConstants.BingUrlPattern,searchModel.SearchLimit);
            var totalCount = positions.Count();

            return new SearchResultModel
            {
                TotalCount = totalCount,
                Positions = totalCount > 0 ? string.Join(", ", positions) : "0"
            };

        }

        private async Task<string> SearchBingAsync(string query, int page)
        {
            var searchUrl = $"search?q={Uri.EscapeDataString(query)}&first={page * CommonConstants.LimitItemPerPage + 1}";
            var result = string.Empty;

            try
            {
                var response = await _httpClient.GetAsync(searchUrl);
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("[BingSearchClient] - Request error: {message}", ex.Message);
                throw;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError("[BingSearchClient] - Request timed out: {message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError("[BingSearchClient] - Request timed out: {message}", ex.Message);
                throw;
            }

            return result;
        }
    }
}
