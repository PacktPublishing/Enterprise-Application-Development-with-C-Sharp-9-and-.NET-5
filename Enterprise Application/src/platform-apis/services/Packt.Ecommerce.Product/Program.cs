// "//-----------------------------------------------------------------------".
// <copyright file="Program.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Packt.Ecommerce.Product.Startup>()
            .ConfigureKestrel((options) =>
            {
                options.AddServerHeader = false;
            });
        }).Build().Run();