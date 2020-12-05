using System;
using System.Threading.Tasks;

namespace Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(() => Task1(1)) // 1+2 = 3
                .ContinueWith(a => Task2(a.Result)) // 3*2 = 6
                    .ContinueWith(b => Task3(b.Result))// 6-2=4
                        .ContinueWith(c => Console.WriteLine(c.Result));
            Console.ReadLine();
        }

        public static int Task1(int a) => a + 2;
        public static int Task2(int a) => a * 2;
        public static int Task3(int a) => a - 2;

    }
}
