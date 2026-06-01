
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace PaperaX.Infrastructure.Redis
{
    public class OtpRedisService
    {
        public readonly IDatabase _database;

        public OtpRedisService(RedisConnection redisConnection)
        {
            _database = redisConnection.Connection.GetDatabase();
        }

        public async Task StoreOtpAsync(string email, string otp)
        {
            await _database.StringSetAsync($"otp:{email}", otp, TimeSpan.FromMinutes(5));
        }

        public async Task<string?> GetOtpAsync(string email)
        {
            return await _database.StringGetAsync($"otp:{email}");
        }

        public async Task RemoveOtpAsync(string email)
        {
             await _database.KeyDeleteAsync($"otp:{email}");
        }
    }
}
