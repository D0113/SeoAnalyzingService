using Microsoft.Extensions.Caching.Memory;
using SeoAnalyzing.Infrastructure.Interfaces.Services;
using SeoAnalyzing.Model.Search;

namespace SeoAnalyzing.Infrastructure.Core.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public SearchResponseModel? Set(string key, SearchResponseModel? value, TimeSpan absoluteExpirationRelativeToNow)
        {
            return _memoryCache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow
            });
        }

        public bool TryGetValue(string key, out SearchResponseModel? value)
        {
            if (_memoryCache.TryGetValue(key, out SearchResponseModel? cachedValue))
            {
                value = cachedValue;

                return true;
            }

            value = null;

            return false;
        }
    }
}
