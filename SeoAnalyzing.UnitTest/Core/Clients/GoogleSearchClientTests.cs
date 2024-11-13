using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using SeoAnalyzing.Common.Constants;
using SeoAnalyzing.Infrastructure.Core.Clients;
using SeoAnalyzing.Infrastructure.Interfaces.Clients;
using SeoAnalyzing.UnitTest.MockData;

namespace SeoAnalyzing.UnitTest.Core.Client
{
    public class GoogleSearchClientTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly Mock<ILogger<GoogleSearchClient>> _loggerMock;
        private readonly IGoogleSearchClient _googleSearchClient;
        private const string SearchUrl = "//www.example.com";

        public GoogleSearchClientTests()
        {
            _loggerMock = new Mock<ILogger<GoogleSearchClient>>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(); 
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri(CommonConstants.GoogleUrl)
            };

            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock
                .Setup(x => x.CreateClient(CommonConstants.GoogleClient))
                .Returns(_httpClient);

            _googleSearchClient = new GoogleSearchClient(httpClientFactoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task SearchAsync_ReturnsExpectedResults()
        {
            // Arrange
            var searchModel = SearchRequestModelMocks.GenerateSearchRequestModel(SearchUrl);

            var htmlString = $"<html><body><div data-id=\"atritem-https:{SearchUrl}\"></div><div data-id=\"atritem-https:{SearchUrl}\"></div><div data-id=\"atritem-https:{SearchUrl}\"></div></body></html>";
            var expectedPositions = new List<int> { 1, 2, 3 };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()).
                ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(htmlString)
                });

            // Act
            var result = await _googleSearchClient.SearchAsync(searchModel);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPositions.Count, result.TotalCount);
            Assert.Equal(string.Join(", ", expectedPositions), result.Positions);
        }

        [Fact]
        public async Task SearchAsync_ThrowsHttpRequestException()
        {
            // Arrange
            var searchModel = SearchRequestModelMocks.GenerateSearchRequestModel(SearchUrl);
            var errorMessage = "Error!";

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException(errorMessage));

            // Act & Assert
            var exceptionResult = await Assert.ThrowsAsync<HttpRequestException>(() => _googleSearchClient.SearchAsync(searchModel));
            Assert.Equal(exceptionResult.Message, errorMessage);
        }

        [Fact]
        public async Task SearchAsync_ThrowsTaskCanceledException()
        {
            // Arrange
            var searchModel = SearchRequestModelMocks.GenerateSearchRequestModel(SearchUrl);
            var errorMessage = "Error!";

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new TaskCanceledException(errorMessage));

            // Act & Assert
            var exceptionResult = await Assert.ThrowsAsync<TaskCanceledException>(() => _googleSearchClient.SearchAsync(searchModel));
            Assert.Equal(exceptionResult.Message, errorMessage);
        }

        [Fact]
        public async Task SearchAsync_ThrowsException()
        {
            // Arrange
            var searchModel = SearchRequestModelMocks.GenerateSearchRequestModel(SearchUrl);
            var errorMessage = "Exception!";

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new Exception(errorMessage));

            // Act & Assert
            var exceptionResult = await Assert.ThrowsAsync<Exception>(() => _googleSearchClient.SearchAsync(searchModel));
            Assert.Equal(exceptionResult.Message, errorMessage);
        }
    }

}
