using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.DataSource.DeviceLogDataSource;
using SmartHomeManager.DataSource.DeviceLogDataSource.Mocks;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Mocks;
using SmartHomeManager.DataSource.AccountDataSource;
using SmartHomeManager.DataSource.NotificationDataSource;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;

namespace SmartHomeManager.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // For allowing React to communicate with API
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });

            builder.Services.AddControllers();

            #region DEPENDENCY INJECTIONS
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // DEVICELOG

            
            builder.Services.AddScoped<IDeviceLogRepository, DeviceLogRepository>();
            builder.Services.AddScoped<IProfileService, ProfileRepositoryMock>();
            //builder.Services.AddScoped<IDeviceWattsService, DeviceLogRepository>();
            // Inject dependencies for Notification Repository, so all implementations of IGenericRepository<Notification> will use the NotificationRepository implementation...
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddScoped<IGenericRepository<Account>, MockAccountRepository>();

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

            app.UseCors();

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

                await DeviceLogSeedData.Seed(context);
                await NotificationRepositorySeeder.Seed(context);
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