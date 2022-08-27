using CPT.ApplicationCore.Services.Account.Interface;
using CPT.ApplicationCore.Services.Account.Service;
using CPT.Domain.Entities;
using CPT.Infrastructure.MapperConfig;
using CPT.Infrastructure.TokenService.Interface;
using CPT.Infrastructure.TokenService.Service;
using CPT.Persistence.Context;
using CPT.WebApp.Configurations;
using CPT.WebApp.Services.Account;
using CPT.WebApp.Services.Transaction;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CPT.WebApp
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
            services.AddControllersWithViews();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddDbContext<CPTDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("CPTDbConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.SignIn.RequireConfirmedAccount = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<CPTDbContext>()
            .AddDefaultTokenProviders();


            var appSettings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json", true)
            .AddEnvironmentVariables()
            .Build();

            services.Configure<AppSettingsConfig>(appSettings.GetSection(nameof(AppSettingsConfig)));
            services.AddScoped<LoginService>();
            services.AddScoped<CustomerTransactions>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                   // pattern: "{controller=Home}/{action=Index}/{id?}");
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
