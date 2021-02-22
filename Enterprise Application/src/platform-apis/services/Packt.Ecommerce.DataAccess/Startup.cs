// "//-----------------------------------------------------------------------".
// <copyright file="Startup.cs" company="Packt">
// Copyright (c) 2020 Packt Corporation. All rights reserved.
// </copyright>
// "//-----------------------------------------------------------------------".
namespace Packt.Ecommerce.DataAccess
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Packt.Ecommerce.Common.Middlewares;
    using Packt.Ecommerce.Common.Options;
    using Packt.Ecommerce.DataAccess.Extensions;
    using Packt.Ecommerce.DataStore;

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
            services.AddControllers();
            services.Configure<ApplicationSettings>(this.Configuration.GetSection("ApplicationSettings"));
            services.Configure<DatabaseSettingsOptions>(this.Configuration.GetSection("CosmosDB"));
            string accountEndPoint = this.Configuration.GetValue<string>("CosmosDB:AccountEndPoint");
            string authKey = this.Configuration.GetValue<string>("CosmosDB:AuthKey");
            CosmosClientOptions options = new ()
            {
                SerializerOptions = new () { IgnoreNullValues = true },
            };
            services.AddSingleton(s => new CosmosClient(accountEndPoint, authKey, options));
            services.AddRepositories();

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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
        }

        /// <summary>
        /// // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Data Access API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
