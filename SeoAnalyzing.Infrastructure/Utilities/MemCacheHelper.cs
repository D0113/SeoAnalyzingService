using SeoAnalyzing.Common.Enums;
using SeoAnalyzing.Model.Search;

namespace SeoAnalyzing.Infrastructure.Utilities
{
    public static class MemCacheHelper
    {
        public static string GenerateCacheKey(SearchRequestModel model, SearchEngine engine)
        {
            return $"{model.SearchQuery}_{model.SearchUrl}-{engine}";
        }
    }
}
