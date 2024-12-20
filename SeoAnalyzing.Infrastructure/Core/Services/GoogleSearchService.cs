﻿using Microsoft.Extensions.Logging;
using SeoAnalyzing.Common.Enums;
using SeoAnalyzing.Infrastructure.Interfaces.Clients;
using SeoAnalyzing.Infrastructure.Interfaces.Services;
using SeoAnalyzing.Infrastructure.Utilities;
using SeoAnalyzing.Model.Search;


namespace SeoAnalyzing.Infrastructure.Core.Services
{
    public class GoogleSearchService : IGoogleSearchService
    {
        private readonly IGoogleSearchClient _googleSearchClient;
        private readonly IMemoryCacheService _memoryCacheService;
        private readonly ILogger<GoogleSearchService> _logger;

        public GoogleSearchService(IGoogleSearchClient googleSearchClient, IMemoryCacheService memoryCacheService, ILogger<GoogleSearchService> logger)
        {
            _googleSearchClient = googleSearchClient;
            _memoryCacheService = memoryCacheService;
            _logger = logger;
        }

        public async Task<SearchResponseModel> SearchAsync(SearchRequestModel searchModel)
        {
            var cacheKey = MemCacheHelper.GenerateCacheKey(searchModel, SearchEngine.Google);
            var searchEngine = SearchEngine.Google.ToString();

            _logger.LogInformation("[GoogleSearchService] - Try to get cache value");

            if (!_memoryCacheService.TryGetValue(cacheKey, out SearchResponseModel? cachedValue))
            {
                _logger.LogInformation("[GoogleSearchService] - Start {mode} search - query: {query} - Url: {url}, Limit: {limit}",
                    searchEngine, searchModel.SearchQuery, searchModel.SearchUrl, searchModel.SearchLimit);

                var searchResult = await _googleSearchClient.SearchAsync(searchModel);

                cachedValue = new SearchResponseModel
                {
                    TotalCount = searchResult.TotalCount,
                    Positions = searchResult.Positions,
                    SearchEngine = searchEngine,
                };

                _logger.LogInformation("[GoogleSearchService] - Start cache search result.");
                _memoryCacheService.Set(cacheKey, cachedValue, TimeSpan.FromHours(1));

                return cachedValue;
            }

            return cachedValue ?? new SearchResponseModel
            {
                TotalCount = 0,
                SearchEngine = searchEngine,
                Positions = "0",
            };
        }
    }
}
