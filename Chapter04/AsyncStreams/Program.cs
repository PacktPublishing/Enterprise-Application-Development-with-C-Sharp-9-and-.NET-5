using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncStreams
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await foreach (int i in GetEmployeeIDAsync(5))
            {
                Console.WriteLine(i);
            }
            Console.ReadLine();
        }

        static async IAsyncEnumerable<int> GetEmployeeIDAsync(int input)
        {
            int id = 0;
            List<int> tempID = new List<int>();
            for (int i = 0; i < input; i++)
            {
                await Task.Delay(1000);
                id += i; // Hypothetical calculation
                yield return id;
            }
        }


    }
}
