using RedisPubSubExample.Core.Redis.MessageBroker.Implementations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisPubSubExample.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var channelName = "CartPreviewCreated";
            var producer = new RedisProducer();
            var eventRaiserTasks = new List<Task>();

            var consumer = new RedisConsumer();
            var queue = consumer.Consume(channelName);

            var messageCount = 0;

            queue.OnMessage(x =>
            {
                ClearCurrentConsoleLine();
                var content = $"<{x.Channel}>: {x.Message} delivered at {DateTime.Now.ToString("dd.MM.yyyy-HH:mm:ss")}>";
                Console.WriteLine(content);
                Console.WriteLine($"{++messageCount} messages delivered.");
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            });

            for (int i = 0; i < 1000; i++)
            {
                eventRaiserTasks.Add(producer.ProduceAsync(channelName, $"ThrowTime:{DateTime.Now.ToString("dd.MM.yyyy-HH:mm:ss")} at order {i + 1}. with {Guid.NewGuid()}."));
            }
            Task.WhenAll();
            Console.ReadKey();
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
