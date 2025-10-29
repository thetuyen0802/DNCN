using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Redis
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task GetAsync(string key)
        {
             await _cache.GetStringAsync(key);
        }

        public async Task Remove(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            var option = new DistributedCacheEntryOptions();
            if (expiry.HasValue)
            {
                option.AbsoluteExpirationRelativeToNow = expiry.Value;
            }
            await _cache.SetStringAsync(key, value, option);
        }
    }
}
