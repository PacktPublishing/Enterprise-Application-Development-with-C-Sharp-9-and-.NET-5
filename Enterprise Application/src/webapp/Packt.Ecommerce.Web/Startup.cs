// "//-----------------------------------------------------------------------".
// <copyright file="Startup.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".

namespace Packt.Ecommerce.Web
{
    using System;
    using System.Net.Http;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Packt.Ecommerce.Common.Options;
    using Packt.Ecommerce.Web.Contracts;
    using Packt.Ecommerce.Web.Services;
    using Polly;
    using Polly.Extensions.Http;

    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationSettings>(this.Configuration.GetSection("ApplicationSettings"));
            services.AddControllersWithViews();
            services.AddHttpClient<IECommerceService, ECommerceService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(RetryPolicy()) // Retry policy
                .AddPolicyHandler(CircuitBreakerPolicy()); // Circuit breakerpolicy
            services.AddScoped<IECommerceService, ECommerceService>();

            // App insights.
            string appinsightsInstrumentationKey = this.Configuration.GetValue<string>("ApplicationSettings:InstrumentationKey");

            if (!string.IsNullOrWhiteSpace(appinsightsInstrumentationKey))
            {
                services.AddLogging(logging =>
                {
                    logging.AddApplicationInsights(appinsightsInstrumentationKey);
                });
                services.AddApplicationInsightsTelemetry(appinsightsInstrumentationKey);
            }
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePagesWithReExecute("/Products/Error/{0}");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Products/Error/500");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Products}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// The Retry policy.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        private static IAsyncPolicy<HttpResponseMessage> RetryPolicy()
        {
            Random random = new Random();
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(
                5,
                retry => TimeSpan.FromSeconds(Math.Pow(2, retry))
                                  + TimeSpan.FromMilliseconds(random.Next(0, 100)));
            return retryPolicy;
        }

        /// <summary>
        /// Gets the circuit breaker policy.
        /// </summary>
        /// <returns>HttpResponseMessage.</returns>
        private static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        }
    }
}
