using System;
using System.Threading;

namespace SimpleThreading
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread loadFileFromDisk = new Thread(LoadFileFromDisk);
            loadFileFromDisk.Start();
            Thread fetchDataFromAPI = new Thread(FetchDataFromAPI);
            fetchDataFromAPI.Start("https://dummy/v1/api"); //Parameterized method
            Console.ReadLine();
        }

        static void FetchDataFromAPI(object apiURL)
        {
            Thread.Sleep(2000);
            Console.WriteLine("data returned from API");
        }

        static void LoadFileFromDisk()
        {
            Thread.Sleep(2000);
            Console.WriteLine("File loaded from disk");
        }

    }
}
