using SeoAnalyzing.Model.Search;

namespace SeoAnalyzing.Infrastructure.Interfaces.Clients
{
    public interface IGoogleSearchClient
    {
        public Task<SearchResultModel> SearchAsync(SearchRequestModel searchModel);
    }
}
