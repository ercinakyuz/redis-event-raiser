using System;
using System.Threading.Tasks;

namespace RedisPubSubExample.Core.MessageBroker.Abstractions
{
    public interface IConsumer
    {
        void Consume(string channel);
    }

    public interface IConsumer<T>
    {
        T Consume(string channelName);
        Task<T> ConsumeAsync(string channelName);
    }
}
