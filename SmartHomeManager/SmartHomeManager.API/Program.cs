using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.Common;
using Microsoft.Extensions.DependencyInjection;
using SmartHomeManager.DataSource.DeviceLogDataSource;

namespace SmartHomeManager.API
{
    public class Program
    {

        public async void  Main(string[] args)
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
           

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                // in order to use await in a method, the caller method must be async as well
                await DeviceLogSeedData.Seed(context);
            }
            catch (Exception e)
            {
                // Tells the logger to log against the Program class
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(e, "An error occurred during migration.");
            }

            app.Run();


        }
    }
}