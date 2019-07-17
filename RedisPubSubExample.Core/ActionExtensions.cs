using System;
using System.Threading.Tasks;

namespace RedisPubSubExample.Core
{
    public static class ActionExtensions
    {
        public static T RetryOnException<T>(this Func<T> @delegate, int times, TimeSpan delay)
        {
            T returnValue;
            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    returnValue = @delegate.Invoke();
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

        public static void RetryOnException(this Action @delegate, int times, TimeSpan delay)
        {
            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    @delegate();
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
