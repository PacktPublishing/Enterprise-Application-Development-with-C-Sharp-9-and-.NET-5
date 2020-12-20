using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Packt.Ecommerce.UserManagement.Startup>()
            .ConfigureKestrel((options) =>
            {
                options.AddServerHeader = false;
            });
        }).Build().Run();