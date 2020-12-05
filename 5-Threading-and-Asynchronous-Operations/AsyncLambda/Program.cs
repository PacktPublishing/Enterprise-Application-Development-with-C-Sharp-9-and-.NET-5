using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncLambda
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //// Sample 1
            //long elapsedTime = AsyncLambda(async () =>
            //{
            //    await Task.Delay(1000);
            //});

            // Sample 2
            long elapsedTime = await AsyncLambda(() => Task.Delay(1000));
            Console.WriteLine(elapsedTime);
            Console.ReadLine();
        }

        //// Sample 1
        //private static long AsyncLambda(Action a)
        //{
        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        a();
        //    }
        //    return sw.ElapsedMilliseconds;
        //}

        // Sample 2
        private async static Task<long> AsyncLambda(Func<Task> a)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 10; i++)
            {
                await a();
            }
            return sw.ElapsedMilliseconds;
        }


    }
}
