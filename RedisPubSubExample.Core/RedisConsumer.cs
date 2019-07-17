using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace RedisPubSubExample.Core
{
    public class RedisConsumer : IConsumer<ChannelMessageQueue>
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private ISubscriber _consumer;
        public RedisConsumer()
        {
            _redisConnection = RedisConnectionProvider.GetConnection("localhost");
            _consumer = _redisConnection?.GetSubscriber();
        }
        public ChannelMessageQueue Consume(string channelName)
        {
            ChannelMessageQueue queue = null;
            if (_consumer != null)
            {
                Func<ChannelMessageQueue> function = () => _consumer.Subscribe(channelName);
                queue = function.RetryOnException(3, TimeSpan.FromMilliseconds(10));
            }
          
            return queue;
        }
        public async Task<ChannelMessageQueue> ConsumeAsync(string channelName)
        {
            ChannelMessageQueue queue = null;
            if (_consumer != null)
            {
                Func<Task<ChannelMessageQueue>> function = () => _consumer.SubscribeAsync(channelName);
                queue = await function.RetryOnExceptionAsync(3, TimeSpan.FromMilliseconds(10));
            }

            return queue;
        }
    }
}
