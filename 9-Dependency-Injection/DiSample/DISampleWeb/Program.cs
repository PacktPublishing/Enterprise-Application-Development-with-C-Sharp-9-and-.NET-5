using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Unity.Microsoft.DependencyInjection;

namespace DISampleWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webhost = CreateHostBuilder(args).Build();
            webhost.Services.CreateScope();

            webhost.Run();
        }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //.UseUnityServiceProvider()
            .UseDefaultServiceProvider(opt => { opt.ValidateScopes = true; })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
