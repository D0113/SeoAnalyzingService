using SeoAnalyzing.Model.Search;

namespace SeoAnalyzing.UnitTest.MockData
{
    public class SearchRequestModelMocks
    {
        public static SearchRequestModel GenerateSearchRequestModel(string searchUrl)
        {
            return new SearchRequestModel
            {
                SearchQuery = "example",
                SearchLimit = 10,
                SearchUrl = searchUrl
            };
        }
}
}
