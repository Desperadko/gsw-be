using GSW_Data;
using Microsoft.EntityFrameworkCore;

namespace GSW.Extensions
{
    public static class ApplicationExtensions
    {
        public async static Task<WebApplication> UpdateMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<GSWDbContext>();
                await context.Database.MigrateAsync();
            }

            return app;
        }

        public static WebApplication ConfigureSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }
    }
}
