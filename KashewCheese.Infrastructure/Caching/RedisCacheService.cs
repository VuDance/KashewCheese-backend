using Application.Interfaces;
using KashewCheese.Application.DTO;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Redis
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDistributedCache _distributedCache;

        public RedisCacheService(IConnectionMultiplexer redis,IDistributedCache distributedCache)
        {
            _redis = redis;
            _distributedCache = distributedCache;
        }

        public async Task SetCacheAsync(string key, object value, TimeSpan expiration)
        {
            if(value == null)
            {
                return;
            }
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(value,settings);
            await _distributedCache.SetStringAsync(key, json,new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = expiration,
            });
        }

        public async Task<string> GetCacheAsync(string key)
        {
            var cacheRes= await _distributedCache.GetStringAsync(key);
            return cacheRes;
        }

        public async Task RemoveCacheAsync(string key)
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync(key);
        }

        public string GenerateCacheKey(string prefix,IDictionary<string,string>? queryParamaters, string? claims)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{prefix}:");
            if (claims != null)
            {
                keyBuilder.Append($"|{claims}");
            }

            if (queryParamaters!=null && queryParamaters.Count>0)
            {
                foreach (var (key, value) in queryParamaters.OrderBy(x => x.Key))
                {
                    keyBuilder.Append($"|{key}---{value}");
                }
            }

            return keyBuilder.ToString();
        }
        public string ConvertData(string data)
        {
            data = data.Replace("\\\\\\\"", "\"").Trim('"');
            return data.Replace("\\\"", "\"").Trim('"');
        }
    }

}
