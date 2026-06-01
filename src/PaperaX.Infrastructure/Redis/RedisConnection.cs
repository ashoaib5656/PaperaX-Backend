using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace PaperaX.Infrastructure.Redis
{
    public class RedisConnection
    {
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public RedisConnection(string conectionStrng)
        {
            var parsedConnectionString = ParseConnectionString(conectionStrng);
            var options = ConfigurationOptions.Parse(parsedConnectionString);
            options.AbortOnConnectFail = false; // Never crash the API if Redis is offline
            options.ConnectRetry = 3;
            options.ConnectTimeout = 5000;

            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options));
        }

        public ConnectionMultiplexer Connection => _lazyConnection.Value;

        private static string ParseConnectionString(string rawInput)
        {
            if (string.IsNullOrWhiteSpace(rawInput))
            {
                return "localhost:6379";
            }

            if (rawInput.StartsWith("redis://") || rawInput.StartsWith("rediss://"))
            {
                try
                {
                    var uri = new Uri(rawInput);
                    var host = uri.Host;
                    var port = uri.Port > 0 ? uri.Port : 6379;
                    var userInfo = uri.UserInfo;
                    var password = string.Empty;

                    if (!string.IsNullOrEmpty(userInfo))
                    {
                        var parts = userInfo.Split(':');
                        password = parts.Length > 1 ? parts[1] : parts[0];
                    }

                    var isSsl = rawInput.StartsWith("rediss://");
                    var config = $"{host}:{port}";
                    
                    if (!string.IsNullOrEmpty(password))
                    {
                        config += $",password={password}";
                    }
                    if (isSsl)
                    {
                        config += ",ssl=true";
                    }
                    return config;
                }
                catch
                {
                    // Fallback to raw if parsing fails
                }
            }

            return rawInput;
        }
    }
}
