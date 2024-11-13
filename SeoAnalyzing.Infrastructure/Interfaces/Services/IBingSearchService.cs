using SeoAnalyzing.Model.Search;

namespace SeoAnalyzing.Infrastructure.Interfaces.Services
{
    public interface IBingSearchService
    {
        public Task<SearchResponseModel> SearchAsync(SearchRequestModel searchModel);
    }
}
