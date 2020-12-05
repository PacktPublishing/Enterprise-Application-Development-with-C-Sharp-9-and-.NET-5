using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Cancellation
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Task<string> dataFromAPI;

            //// Sample 1
            //try
            //{
            //    dataFromAPI = Task.Factory.StartNew<string>(() => FetchDataFromAPI(new List<string> {
            //                    "https://foo.com","https://foo1.com","https://foo2.com","https://foo3.com",
            //                    "https://foo4.com",
            //                }, token));
            //    Thread.Sleep(3000);
            //    cts.Cancel(); //Trigger cancel notification to cancellation token
            //    dataFromAPI.Wait(); // Wait for task completion
            //    Console.WriteLine(dataFromAPI.Result); // If task is completed display message accordingly
            //}
            //catch (AggregateException agex)
            //{
            //    // Handle exception
            //    List<Tuple<string, string>> errors = agex.Flatten().InnerExceptions.Select(x => new Tuple<string, string>(x.Message, x.StackTrace)).ToList();
            //    int counter = 0;
            //    foreach (Tuple<string, string> error in errors)
            //    {
            //        counter++;
            //        Console.WriteLine($"{counter}).Error - {error.Item1} \n Innerstack \n { error.Item2} \n");
            //    }
            //}

            // Sample 2
            try
            {
                // Running the long running task                
                dataFromAPI = FetchDataFromAPIWithCancellation(new List<string>
                {
                        "https://foo.com",
                        "https://foo1.com",
                        "https://foo2.com",
                        "https://foo3.com",
                        "https://foo4.com",
                    }, token);
                var result = dataFromAPI.Result;

                Console.WriteLine("Result {0}", result);
                Console.WriteLine("Press enter to continue");
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Task was cancelled");
            }

        }

        public static string FetchDataFromAPI(List<string> apiURL, CancellationToken token)
        {
            Console.WriteLine("Task started");
            int counter = 0;
            foreach (string url in apiURL)
            {
                if (token.IsCancellationRequested)
                {
                    throw new TaskCanceledException($"data from API returned up to iteration {counter}");
                    //throw new OperationCanceledException($"data from API returned up to iteration {counter}"); // Alternate exception with same result
                    //break; // To handle manually 
                }
                Thread.Sleep(1000);
                Console.WriteLine($"data retrieved from {url} for iteration {counter}");
                counter++;
            }
            return $"data from API returned up to iteration {counter}";
        }

        private static Task<string> FetchDataFromAPIWithCancellation(List<string> apiURL, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<string>();

            tcs.TrySetCanceled(cancellationToken);

            // calling overload of long running operation that doesn’t support cancellation token
            var dataFromAPI = Task.Factory.StartNew(() => FetchDataFromAPI(apiURL));

            // Wait for the first task to complete
            var outputTask = Task.WhenAny(dataFromAPI, tcs.Task);

            return outputTask.Result;
        }

        public static string FetchDataFromAPI(List<string> apiURL)
        {
            Console.WriteLine("Task started");
            int counter = 0;
            foreach (string url in apiURL)
            {
                Thread.Sleep(2000);
                Console.WriteLine($"data retrieved from {url} for iteration {counter}");
                counter++;
            }
            return $"data from API return upto iteration {counter}";
        }


    }
}
