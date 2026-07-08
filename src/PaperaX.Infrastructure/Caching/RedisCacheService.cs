using PaperaX.Application.Interfaces;
using PaperaX.Infrastructure.Redis;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaperaX.Infrastructure.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisCacheService(RedisConnection redisConnection)
        {
            _redis = redisConnection.Connection;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var db = _redis.GetDatabase();
            var value = await db.StringGetAsync(key);

            if (value.IsNullOrEmpty)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(value!);
        }

        public async Task RemoveAsync(string key)
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null)
        {
            var db = _redis.GetDatabase();
            var serializedValue = JsonSerializer.Serialize(value);
            if (expirationTime.HasValue)
            {
                await db.StringSetAsync(key, serializedValue, expirationTime.Value);
            }
            else
            {
                await db.StringSetAsync(key, serializedValue);
            }
        }
    }
}
