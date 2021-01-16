using Autofac;
using DISampleWeb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Unity;

namespace DISampleWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Register as Scoped 
            services.AddScoped<IScopedService, ScopedService>();
            //Register as Singleton
            services.AddSingleton<ISingletonService, SingletonService>();
            //Register as Transient
            services.AddTransient<ITransientService, TransientService>();


            services.AddScoped<IWeatherForcastService, WeatherForcastService>();
            services.Replace(ServiceDescriptor.Scoped<IWeatherForcastService, WeatherForcastServiceV2>());

            //Removes the first registration of IWeatherForcastService .
            services.Remove(ServiceDescriptor.Scoped<IWeatherForcastService, WeatherForcastService>());

            //Removes all the registrations of IWeatherForcastService.
            services.RemoveAll<IWeatherForcastService>();
            
            services.AddControllersWithViews();
        }

        //public void ConfigureContainer(IUnityContainer container)
        //{
        //    // Register WeatherForcastService
        //    container.RegisterType<IWeatherForcastService, WeatherForcastService>();
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            applicationLifetime.ApplicationStopping.Register(() => {
                // Dispose those objects instantiated by the developer
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
