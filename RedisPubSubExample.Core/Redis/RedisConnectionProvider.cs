using RedisPubSubExample.Core.Extensions;
using StackExchange.Redis;
using System;

namespace RedisPubSubExample.Core
{
    public static class RedisConnectionProvider
    {
        public static ConnectionMultiplexer GetConnection(string configuration)
        {
            Func<ConnectionMultiplexer> function = () => ConnectionMultiplexer.Connect(configuration);
            return function.RetryOnException(3, TimeSpan.FromMilliseconds(10));
        }
    }
}
