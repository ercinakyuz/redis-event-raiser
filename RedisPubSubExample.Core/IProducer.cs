using System.Threading.Tasks;

namespace RedisPubSubExample.Core
{
    public interface IProducer
    {
        void Produce(string channelName, string message);
        Task ProduceAsync(string channelName, string message);
    }
}
