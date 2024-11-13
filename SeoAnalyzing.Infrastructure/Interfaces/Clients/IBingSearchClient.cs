using SeoAnalyzing.Model.Search;

namespace SeoAnalyzing.Infrastructure.Interfaces.Clients
{
    public interface IBingSearchClient
    {
        public Task<SearchResultModel> SearchAsync(SearchRequestModel searchModel);

    }
}
