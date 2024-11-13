using Microsoft.Extensions.Logging;
using Moq;
using SeoAnalyzing.Common.Enums;
using SeoAnalyzing.Infrastructure.Core.Services;
using SeoAnalyzing.Infrastructure.Interfaces.Clients;
using SeoAnalyzing.Infrastructure.Interfaces.Services;
using SeoAnalyzing.Infrastructure.Utilities;
using SeoAnalyzing.Model.Search;
using SeoAnalyzing.UnitTest.MockData;


namespace SeoAnalyzing.UnitTest.Core.Services
{
    public class BingSearchServiceTests
    {
        private readonly Mock<IBingSearchClient> _bingSearchClientMock;
        private readonly Mock<IMemoryCacheService> _memoryCacheServiceMock;
        private readonly Mock<ILogger<BingSearchService>> _loggerMock;
        private readonly IBingSearchService _bingSearchService;
        private const string SearchUrl = "//www.example.com";

        public BingSearchServiceTests()
        {
            _bingSearchClientMock = new Mock<IBingSearchClient>();
            _memoryCacheServiceMock = new Mock<IMemoryCacheService>();
            _loggerMock = new Mock<ILogger<BingSearchService>>();
            _bingSearchService = new BingSearchService(_bingSearchClientMock.Object, _memoryCacheServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task SearchAsync_ReturnsCachedValue_WhenCacheHit()
        {
            // Arrange
            var searchModel = SearchRequestModelMocks.GenerateSearchRequestModel(SearchUrl);
            var cacheKey = MemCacheHelper.GenerateCacheKey(searchModel, SearchEngine.Bing);
            var expectedResponse = SearchResponseModelMocks.GenerateResponseModel(SearchEngine.Bing.ToString());

            _memoryCacheServiceMock.Setup(mc => mc.TryGetValue(cacheKey, out expectedResponse)).Returns(true);

            // Act
            var result = await _bingSearchService.SearchAsync(searchModel);

            // Assert
            Assert.Equal(result.SearchEngine, expectedResponse.SearchEngine);
            Assert.Equal(result.Position, expectedResponse.Position);
            Assert.Equal(result.TotalCount, expectedResponse.TotalCount);
        }

        [Fact]
        public async Task SearchAsync_ReturnsDefaultValue_WhenCacheHit()
        {
            // Arrange
            var searchModel = SearchRequestModelMocks.GenerateSearchRequestModel(SearchUrl);
            var cacheKey = MemCacheHelper.GenerateCacheKey(searchModel, SearchEngine.Bing);
            var expectedResponse = SearchResponseModelMocks.GenerateResponseModelDefault(SearchEngine.Bing.ToString());
            SearchResponseModel nullValue = null;

            _memoryCacheServiceMock.Setup(mc => mc.TryGetValue(cacheKey, out nullValue)).Returns(true);

            // Act
            var result = await _bingSearchService.SearchAsync(searchModel);

            // Assert
            Assert.Equal(result.SearchEngine, expectedResponse.SearchEngine);
            Assert.Equal(result.Position, expectedResponse.Position);
            Assert.Equal(result.TotalCount, expectedResponse.TotalCount);
        }

        [Fact]
        public async Task SearchAsync_CachesResult_WhenCacheMiss()
        {
            // Arrange
            var searchModel = SearchRequestModelMocks.GenerateSearchRequestModel(SearchUrl);
            var cacheKey = MemCacheHelper.GenerateCacheKey(searchModel, SearchEngine.Bing);
            var searchResult = new SearchResultModel
            {
                TotalCount = 2,
                Positions = "1, 2"
            };
            var expectedResponse = SearchResponseModelMocks.GenerateResponseModel(
                                    SearchEngine.Bing.ToString(), searchResult.Positions, searchResult.TotalCount);

            SearchResponseModel nullValue = null;
            _memoryCacheServiceMock.Setup(mc => mc.TryGetValue(cacheKey, out nullValue)).Returns(false);
            _bingSearchClientMock.Setup(client => client.SearchAsync(searchModel)).ReturnsAsync(searchResult);
            _memoryCacheServiceMock.Setup(mc => mc.Set(cacheKey, expectedResponse, It.IsAny<TimeSpan>()));

            // Act
            var result = await _bingSearchService.SearchAsync(searchModel);

            // Assert
            Assert.Equal(result.SearchEngine, expectedResponse.SearchEngine);
            Assert.Equal(result.Position, expectedResponse.Position);
            Assert.Equal(result.TotalCount, expectedResponse.TotalCount);
        }
    }
}

