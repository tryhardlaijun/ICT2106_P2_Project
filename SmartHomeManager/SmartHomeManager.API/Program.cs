using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.Common;
using System.Diagnostics;

namespace SmartHomeManager.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            
            #region DEPENDENCY INJECTIONS
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion DEPENDENCY INJECTIONS

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                logger.LogInformation("hello");

                await AccountSeedData.Seed(context);
                await ProfileSeedData.Seed(context);
            }
            
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred during migration.");
            }

            app.Run();
        }
    }
}