using SeoAnalyzing.Model.Search;

namespace SeoAnalyzing.Infrastructure.Interfaces.Services
{
    public interface IGoogleSearchService
    {
        public Task<SearchResponseModel> SearchAsync(SearchRequestModel searchModel);
    }
}
