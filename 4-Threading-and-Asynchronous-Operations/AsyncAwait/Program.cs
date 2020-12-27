using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DownloadFileAsync("https://github.com/Ravindra-a/largefile/blob/master/README.md",
                @$"{System.IO.Directory.GetCurrentDirectory()}\download.txt");
            Console.WriteLine("File downloaded!!");

            // Call async method
            // Create a new web client object
            //using WebClient webClient = new WebClient();
            //webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64)");
            //webClient.DownloadDataTaskAsync("https://github.com/Ravindra-a/largefile/blob/master/README.md").ContinueWith(dataTask =>
            //{   
            //        using var fileStream = File.OpenWrite(@$"{System.IO.Directory.GetCurrentDirectory()}\download.txt");
            //        {
            //        // Write data in file.
            //        fileStream.WriteAsync(dataTask.Result, 0, dataTask.Result.Length).ContinueWith(writeTask =>
            //            {
            //                Console.WriteLine("File downloaded!!");                            
            //            });
            //        }
            //});


            Console.ReadLine();
        }

        private static async Task DownloadFileAsync(string url, string path)
        {
            // Create a new web client object
            using WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64)");
            byte[] data = await webClient.DownloadDataTaskAsync(url);
            // Write data in file.
            using var fileStream = File.OpenWrite(path);
            {
                await fileStream.WriteAsync(data, 0, data.Length);
            }
        }
    }
}
