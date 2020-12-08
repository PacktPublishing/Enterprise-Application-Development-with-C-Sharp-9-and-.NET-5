using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using AuthSample.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using AuthSample.Core;

namespace AuthSample
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("PremiumContentPolicy",
                    policy => policy.RequireClaim("PremiumUser"));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Over14", policy =>
                    policy.Requirements.Add(new MinimumAgeRequirement(14)));
            });

            services.AddSingleton<IAuthorizationHandler,
                MinimumAgeAuthorizationHandler>();

            services.AddSingleton<IAuthorizationPolicyProvider, MinimumAgePolicyProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            SetupRoles(serviceProvider).Wait();
            SetupUsers(serviceProvider).Wait();

        }

        private async Task SetupRoles(IServiceProvider serviceProvider)
        {
            var rolemanager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Admin", "Support", "User" };
            foreach (var role in roles)
            {
                var roleExist = await rolemanager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await rolemanager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private async Task SetupUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            var adminUser = await userManager.FindByEmailAsync("admin@abc.com");
            if (adminUser == null)
            {
                var newAdminUser = new IdentityUser
                {
                    UserName = "admin@abc.com",
                    Email = "admin@abc.com",
                };

                var result = await userManager
                    .CreateAsync(newAdminUser, "Password@123");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(newAdminUser, "Admin");
            }

            var supportUser = await userManager
                .FindByEmailAsync("support@abc.com");
            if (supportUser == null)
            {
                var newSupportUser = new IdentityUser
                {
                    UserName = "support@abc.com",
                    Email = "support@abc.com",
                };

                var result = await userManager
                    .CreateAsync(newSupportUser, "Password@123");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(newSupportUser, "Support");
            }

            var user = await userManager.FindByEmailAsync("user@abc.com");
            if (user == null)
            {
                var newUser = new IdentityUser
                {
                    UserName = "user@abc.com",
                    Email = "user@abc.com",
                };

                var result = await userManager.CreateAsync(newUser, "Password@123");

                if (result.Succeeded)
                {
                    await userManager
                        .AddToRoleAsync(newUser, "User");
                    await userManager
                        .AddClaimAsync(newUser, new Claim("PremiumUser", "true"));
                    await userManager
                        .AddClaimAsync(newUser, new Claim(ClaimTypes.Country, "US"));

                    await userManager
                        .AddClaimAsync(newUser, new Claim(ClaimTypes.DateOfBirth, DateTimeOffset.Now.AddYears(-25).ToString()));
                }


            }
        }
    }
}
