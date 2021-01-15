using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwaitWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //// Sample 1
            //var task = DownloadFileAsync("https://github.com/Ravindra-a/largefile/blob/master/README.md", @$"{System.IO.Directory.GetCurrentDirectory()}\download.txt");
            //bool fileDownload = task.Result; // Or task.GetAwaiter().GetResult()
            //if (fileDownload)
            //{
            //    MessageBox.Show("file downloaded");
            //}


            // sample 2
            string output = GetAsync().Result; //Blocking code, ideally should cause deadlock.
            MessageBox.Show(output);
        }

        //  Library code         	
        public async Task<string> GetAsync()
        {
            var uri = new Uri("http://www.google.com");
            return await new HttpClient().GetStringAsync(uri).ConfigureAwait(false);
        }


        private async Task<bool> DownloadFileAsync(string url, string path)
        {
            // Create a new web client object
            using WebClient webClient = new WebClient();
            // Add user-agent header to avoid forbidden errors.
            webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64)");
            byte[] data = await webClient.DownloadDataTaskAsync(url);
            // Write data in file.
            using var fileStream = File.OpenWrite(path);
            {
                await fileStream.WriteAsync(data, 0, data.Length);
            }
            return true;
        }

    }
}
