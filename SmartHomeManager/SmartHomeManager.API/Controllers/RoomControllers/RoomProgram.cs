using global::SmartHomeManager.DataSource.RoomRepo;
using global::SmartHomeManager.DataSource;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.DataSource.RoomRepo;
using SmartHomeManager.Domain.Common;

namespace SmartHomeManager.API.Controllers.RoomControllers
{
    public class RoomProgram
    {
        public static async Task RoomMain(string[] args)
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

            // the `using` statement will automatically dispose of the object
            // when the method call ends
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            // try create database and table, if fails, catch and do something
            // it will actually create a database if it does not exist in SQL server
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                // in order to use await in a method, the caller method must be async as well
                // await context.Database.MigrateAsync();
                await RoomSeedData.Seed(context);
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

