using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace TaskBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task dataTask = new Task(() => FetchDataFromAPI("https://www.microsoft.com/en-in/"));
            //dataTask.Start();

            //Task dataTask = Task.Run(() => FetchDataFromAPI("https://www.microsoft.com/en-in/")); 

            //Task t = Task.Factory.StartNew(() => FetchDataFromAPI("https://www.microsoft.com/en-in/"));
            //t.Wait();

            Console.ReadLine();
        }

        public static void FetchDataFromAPI(string apiURL)
        {
            Thread.Sleep(2000);
            Console.WriteLine("data returned from API");
        }

    }
}

