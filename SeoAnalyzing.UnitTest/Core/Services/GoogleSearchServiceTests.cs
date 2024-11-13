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
    public class GoogleSearchServiceTests
    {
        private readonly Mock<IGoogleSearchClient> _googleSearchClientMock;
        private readonly Mock<IMemoryCacheService> _memoryCacheServiceMock;
        private readonly Mock<ILogger<GoogleSearchService>> _loggerMock;
        private readonly IGoogleSearchService _googleSearchService;
        private const string SearchUrl = "//www.example.com";

        public GoogleSearchServiceTests()
        {
            _googleSearchClientMock = new Mock<IGoogleSearchClient>();
            _memoryCacheServiceMock = new Mock<IMemoryCacheService>();
            _loggerMock = new Mock<ILogger<GoogleSearchService>>();

            _googleSearchService = new GoogleSearchService(_googleSearchClientMock.Object, _memoryCacheServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task SearchAsync_ReturnsCachedValue_WhenCacheHit()
        {
            // Arrange
            var searchModel = SearchRequestModelMocks.GenerateSearchRequestModel(SearchUrl);
            var cacheKey = MemCacheHelper.GenerateCacheKey(searchModel, SearchEngine.Google);
            var expectedResponse = SearchResponseModelMocks.GenerateResponseModel(SearchEngine.Google.ToString());

            _memoryCacheServiceMock.Setup(mc => mc.TryGetValue(cacheKey, out expectedResponse)).Returns(true);

            // Act
            var result = await _googleSearchService.SearchAsync(searchModel);

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
            var cacheKey = MemCacheHelper.GenerateCacheKey(searchModel, SearchEngine.Google);
            var expectedResponse = SearchResponseModelMocks.GenerateResponseModelDefault(SearchEngine.Google.ToString());
            SearchResponseModel nullValue = null;

            _memoryCacheServiceMock.Setup(mc => mc.TryGetValue(cacheKey, out nullValue)).Returns(true);

            // Act
            var result = await _googleSearchService.SearchAsync(searchModel);

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
            var cacheKey = MemCacheHelper.GenerateCacheKey(searchModel, SearchEngine.Google);
            var searchResult = new SearchResultModel
            {
                TotalCount = 2,
                Positions = "1, 2"
            };
            var expectedResponse = SearchResponseModelMocks.GenerateResponseModel(
                                    SearchEngine.Google.ToString(), searchResult.Positions, searchResult.TotalCount);

            SearchResponseModel nullValue = null;
            _memoryCacheServiceMock.Setup(mc => mc.TryGetValue(cacheKey, out nullValue)).Returns(false);
            _googleSearchClientMock.Setup(client => client.SearchAsync(searchModel)).ReturnsAsync(searchResult);
            _memoryCacheServiceMock.Setup(mc => mc.Set(cacheKey, expectedResponse, It.IsAny<TimeSpan>()));

            // Act
            var result = await _googleSearchService.SearchAsync(searchModel);

            // Assert
            Assert.Equal(result.SearchEngine, expectedResponse.SearchEngine);
            Assert.Equal(result.Position, expectedResponse.Position);
            Assert.Equal(result.TotalCount, expectedResponse.TotalCount);
        }
    }
}
