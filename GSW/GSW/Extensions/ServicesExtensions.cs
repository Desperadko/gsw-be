using GSW_Core.Repositories.Implementations;
using GSW_Core.Repositories.Interfaces;
using GSW_Core.Services.Implementations;
using GSW_Core.Services.Interfaces;
using GSW_Data;
using Microsoft.EntityFrameworkCore;

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
            services.AddScoped<ITestService, TestService>();

            return services;
        }
    }
}
