using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SeoAnalyzing.Common.Enums;
using SeoAnalyzing.Infrastructure.Interfaces.Clients;
using SeoAnalyzing.Infrastructure.Interfaces.Services;
using SeoAnalyzing.Infrastructure.Utilities;
using SeoAnalyzing.Model.Search;


namespace SeoAnalyzing.Infrastructure.Core.Services
{
    public class BingSearchService : IBingSearchService
    {
        private readonly IBingSearchClient _bingSearchClient ;
        private readonly IMemoryCacheService _memoryCacheService;
        private readonly ILogger<BingSearchService> _logger;

        public BingSearchService(IBingSearchClient bingSearchClient, IMemoryCacheService memoryCacheService, ILogger<BingSearchService> logger)
        {
            _bingSearchClient = bingSearchClient;
            _memoryCacheService = memoryCacheService;
            _logger = logger;
        }

        public async Task<SearchResponseModel> SearchAsync(SearchRequestModel searchModel)
        {
            var cacheKey = MemCacheHelper.GenerateCacheKey(searchModel, SearchEngine.Bing);
            var searchEngine = SearchEngine.Bing.ToString();

            _logger.LogInformation("[BingSearchService] - Try to get cache value");

            if (!_memoryCacheService.TryGetValue(cacheKey, out SearchResponseModel? cachedValue))
            {
                _logger.LogInformation("[BingSearchService] - Start {mode} search - query: {query} - Url: {url}, Limit: {limit}",
                    searchEngine, searchModel.SearchQuery, searchModel.SearchUrl, searchModel.SearchLimit);

                var searchResult = await _bingSearchClient.SearchAsync(searchModel);

                cachedValue = new SearchResponseModel
                {
                    TotalCount = searchResult.TotalCount,
                    Position = searchResult.Positions,
                    SearchEngine = searchEngine,
                };

                _logger.LogInformation("[BingSearchService] - Start cache search result.");
                _memoryCacheService.Set(cacheKey, cachedValue, TimeSpan.FromHours(1));

                return cachedValue;
            }

            return cachedValue ?? new SearchResponseModel
            {
                TotalCount = 0,
                SearchEngine = searchEngine,
                Position = "0",
            };
        }
    }
}
