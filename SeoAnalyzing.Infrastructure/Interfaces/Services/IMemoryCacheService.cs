using SeoAnalyzing.Model.Search;

namespace SeoAnalyzing.Infrastructure.Interfaces.Services
{
    public interface IMemoryCacheService
    {
        public bool TryGetValue(string key, out SearchResponseModel? value);
        public SearchResponseModel? Set(string key, SearchResponseModel? value, TimeSpan absoluteExpirationRelativeToNow);
    }
}
