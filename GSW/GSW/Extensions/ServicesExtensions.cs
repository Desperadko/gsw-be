using FluentValidation;
using GSW_Core.Repositories.Implementations;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Services.Implementations;
using GSW_Core.Services.Interfaces;
using GSW_Core.Validators;
using GSW_Data;
using GSW_Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace GSW.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration
                .GetSection("CorsSettings:AllowedOrigins")
                .Get<string[]>()
                ?? throw new Exception("No allowed orgins for CORS are set in the configuration.");

            services.AddCors(options => 
                options.AddDefaultPolicy(policy =>
                    policy
                    .WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                )
            );

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<GSWDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();

            return services;
        }

        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
            services.AddFluentValidationAutoValidation(configuration =>
            {
                configuration.EnableFormBindingSourceAutomaticValidation = true;
                configuration.EnablePathBindingSourceAutomaticValidation = true;
            });
            return services;
        }
    }
}
