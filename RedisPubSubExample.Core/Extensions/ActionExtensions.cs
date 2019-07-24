using System;
using System.Threading.Tasks;

namespace RedisPubSubExample.Core.Extensions
{
    public static class ActionExtensions
    {
        public static T RetryOnException<T>(this Func<T> function, int times, TimeSpan delay)
        {
            T returnValue;
            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    returnValue = function.Invoke();
                    break;
                }
                catch (Exception e)
                {
                    if (attempts == times)
                        Console.WriteLine(e);

                    Task.Delay(delay).Wait();
                }
            } while (true);
            return returnValue;
        }
        public static async Task<T> RetryOnExceptionAsync<T>(this Func<Task<T>> asyncFunction, int times, TimeSpan delay)
        {
            T returnValue;
            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    returnValue = await asyncFunction.Invoke();
                    break;
                }
                catch (Exception e)
                {
                    if (attempts == times)
                        Console.WriteLine(e);

                    Task.Delay(delay).Wait();
                }
            } while (true);
            return returnValue;
        }

        public static void RetryOnException(this Action action, int times, TimeSpan delay)
        {
            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    action();
                    break;
                }
                catch (Exception e)
                {
                    if (attempts == times)
                        Console.WriteLine(e);

                    Task.Delay(delay).Wait();
                }
            } while (true);
        }
        public static async Task RetryOnExceptionAsync(this Func<Task> asyncFunction, int times, TimeSpan delay)
        {
            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    await asyncFunction();
                    break;
                }
                catch (Exception e)
                {
                    if (attempts == times)
                        Console.WriteLine(e);

                    Task.Delay(delay).Wait();
                }
            } while (true);
        }
    }
}
