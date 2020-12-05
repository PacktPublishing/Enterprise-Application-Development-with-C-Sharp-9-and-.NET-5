using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Enumerable.Range(1, 100).ToList();
            try
            {
                //// sample 1
                //var primeNumbers = (from number in numbers.AsParallel() where CalculatePrime(number) == true select number).ToList();
                //Parallel.ForEach(primeNumbers, (primeNumber) =>
                //{
                //    Console.WriteLine(primeNumber);
                //});

                // Sample 2
                (from i in numbers.AsParallel()
                 where CalculatePrime(i) == true
                 select i).ForAll((primeNumber) => Console.WriteLine(primeNumber));

            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }

            Console.ReadLine();
        }

        static bool CalculatePrime(int num)
        {
            bool isDivisible = false;
            for (int i = 2; i <= num / 2; i++)
            {
                if (num % i == 0)
                {
                    isDivisible = true;
                    break;
                }
            }
            if (!isDivisible && num != 1)
                return true;
            else
                return false;
        }

    }
}
