using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;

using SmartHomeManager.DataSource.AccountDataSource;
using SmartHomeManager.DataSource.ProfileDataSource;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.DataSource.RoomDataSource;
using SmartHomeManager.DataSource.RoomDataSource.Mocks;
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Mocks;
using SmartHomeManager.Domain.Common;
using System.Diagnostics;

namespace SmartHomeManager.API;

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
        builder.Services.AddScoped<IRoomRepository, RoomRepository>();
        builder.Services.AddScoped<IDeviceInformationServiceMock, DeviceRepositoryMock>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
        
        builder.Services.AddScoped<AccountService>();
        builder.Services.AddScoped<EmailService>();
        builder.Services.AddScoped<ProfileService>();

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

        app.Run();
    }
}