
using DotNetEnv;
using GSW.Extensions;

namespace GSW
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            Env.Load();

            builder.Services.AddCors(builder.Configuration);
            builder.Services.AddJWTAuthentication();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddServices(builder.Configuration);
            builder.Services.AddValidation();

            var app = builder.Build();

            app.UseCors();
            app.ConfigureSwagger();
            await app.UpdateMigrations();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}
