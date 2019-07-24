using RedisPubSubExample.Core.Extensions;
using RedisPubSubExample.Core.MessageBroker.Abstractions;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace RedisPubSubExample.Core.Redis.MessageBroker.Implementations
{
    public class RedisProducer : IProducer
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private ISubscriber _producer;
        public RedisProducer()
        {
            _redisConnection = RedisConnectionProvider.GetConnection("localhost");
            _producer = _redisConnection?.GetSubscriber();
        }

        public void Produce(string channelName, string message)
        {
            if (_producer != null)
            {
                Action action = () => _producer.Publish(channelName, message);
                action.RetryOnException(3, TimeSpan.FromMilliseconds(10));
            }

        }
        public async Task ProduceAsync(string channelName, string message)
        {
            if (_producer != null)
            {
                Func<Task> action = () => _producer.PublishAsync(channelName, message);
                await action.RetryOnExceptionAsync(3, TimeSpan.FromMilliseconds(10));
            }

        }


    }
}
