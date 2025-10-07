
using GSW.Extensions;
using GSW_Data;
using Microsoft.EntityFrameworkCore;

namespace GSW
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddServices(builder.Configuration);

            var app = builder.Build();

            app.UseCors();
            app.ConfigureSwagger();
            await app.UpdateMigrations();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}
