using CPT.ApplicationCore.Services.Account.Interface;
using CPT.ApplicationCore.Services.Account.Service;
using CPT.ApplicationCore.Services.Transaction.Interface;
using CPT.ApplicationCore.Services.Transaction.Service;
using CPT.Domain.Entities;
using CPT.Helper.Configuration;
using CPT.Helper.Notification;
using CPT.Infrastructure.MapperConfig;
using CPT.Infrastructure.TokenService.Interface;
using CPT.Infrastructure.TokenService.Service;
using CPT.Persistence.Context;
using CPT.Persistence.Repositories.Customer;
using CPT.Persistence.Repositories.Transactions.Interface;
using CPT.Persistence.Repositories.Transactions.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT.API
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
            services.AddControllers();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer payment transaction API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = "finEdge",
                    //ValidAudience = "http://localhost:18005",
                    // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]  
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("874384$32380349$#YYYCX212834*193@")), //Configuration["JwtToken:SecretKey"]  
                    ClockSkew = TimeSpan.Zero,
                };
            });

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

            services.AddScoped<INotificationTask, NotificationTask>();
            services.AddScoped<TokenConfig>();
            services.AddScoped<CustomerProfileService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<ITransactionService, TransactionService>();


            services.AddScoped<ITransactionRepositoryService, TransactionRepositoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI((c) =>
                {
                    c.DocumentTitle = "Customer Payment Tranaction API - Swagger docs";
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrustBanc Admin.API v1");
                    c.EnableDeepLinking();
                    c.DefaultModelsExpandDepth(0);
                });
            }

            app.UseCors(x => x
           .AllowAnyMethod()
           .AllowAnyHeader()
           .SetIsOriginAllowed(origin => true)
           .AllowCredentials());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "Customer Payment. API v1"));

            Task.Run(() => this.CreateRoles(roleManager)).Wait();
        }
        private async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (string rol in this.Configuration.GetSection("Roles").Get<List<string>>())
            {
                if (!await roleManager.RoleExistsAsync(rol))
                {
                    await roleManager.CreateAsync(new IdentityRole(rol));
                }
            }
        }

    }
}
