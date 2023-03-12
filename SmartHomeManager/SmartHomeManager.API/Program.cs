using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SmartHomeManager.DataSource;
using SmartHomeManager.DataSource.EnergyProfileDataSource;
using SmartHomeManager.DataSource.HistoryDataSource;
using SmartHomeManager.DataSource.HomeSecurityDataSource;
using SmartHomeManager.DataSource.HomeSecurityDeviceDefinitionsDataSource;
using SmartHomeManager.DataSource.HomeSecuritySettingsDataSource;
using SmartHomeManager.DataSource.ProfileDataSource;
using SmartHomeManager.DataSource.RuleHistoryDataSource;
using SmartHomeManager.DataSource.RulesDataSource;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.DirectorDomain.Services;
using SmartHomeManager.Domain.EnergyProfileDomain.Entities;
using SmartHomeManager.Domain.EnergyProfileDomain.Interfaces;
using SmartHomeManager.Domain.EnergyProfileDomain.Services;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using SmartHomeManager.Domain.HomeSecurityDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Services;
using SmartHomeManager.DataSource.AccountDataSource;
using SmartHomeManager.DataSource.DeviceDataSource;
using SmartHomeManager.DataSource.ProfileDataSource;
using SmartHomeManager.DataSource.RoomDataSource;
using SmartHomeManager.DataSource.RoomDataSource.Mocks;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Mocks;
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
using SmartHomeManager.DataSource.BackupDataSource;
using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.BackupDomain.Interfaces;
using SmartHomeManager.Domain.BackupDomain.Services;

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

        #region DEPENDENCY INJECTIONS
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // MODULE 3
        builder.Services.AddScoped<IGenericRepository<History>, DataSource.HistoryDataSource.HistoryRepository>();
        builder.Services.AddScoped<IRuleHistoryRepository<RuleHistory>, RuleHistoryRepository>();
        builder.Services.AddScoped<IGenericRepository<Rule>, RuleRepository>();
        builder.Services.AddScoped<IGenericRepository<EnergyProfile>, EnergyProfileRepository>();
        builder.Services.AddScoped<IGenericRepository<Scenario>, ScenarioRepository>();
        builder.Services.AddScoped<IGetRulesService, GetRulesServices>();
        builder.Services.AddScoped<IGetScenariosService, GetScenariosService>();
        builder.Services.AddScoped<IInformDirectorServices, DirectorServices>();
        builder.Services.AddScoped<IEnergyProfileServices, EnergyProfileServices>();
        builder.Services.AddScoped<IGenericRepository<HomeSecurity>, HomeSecurityRepository>();
        builder.Services.AddScoped<IGenericRepository<HomeSecuritySetting>, HomeSecuritySettingRepository>();
        builder.Services.AddScoped<IHomeSecurityDeviceDefinitionRepository<HomeSecurityDeviceDefinition>, HomeSecurityDeviceDefinitionRepository>();
        builder.Services.AddScoped<IBackupRuleRepository, BackupRuleRepository>();
        builder.Services.AddScoped<IBackupScenarioRepository, BackupScenarioRepository>();
        builder.Services.AddScoped<IBackupService, BackupServices>();

        builder.Services.AddHostedService<DirectorServices>();

        // DEVICE
        builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
        builder.Services.AddScoped<IDeviceTypeRepository, DeviceTypeRepository>();
            

        // DEVICELOG
        builder.Services.AddScoped<IDeviceLogRepository, DeviceLogRepository>();
        // builder.Services.AddScoped<IProfileService, ProfileRepositoryMock>();

        // NOTIFICATION
        // Inject dependencies for Notification Repository, so all implementations of IGenericRepository<Notification> will use the NotificationRepository implementation...
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<IGenericRepository<Account>, MockAccountRepository>();

        // ROOM
        builder.Services.AddScoped<IRoomRepository, RoomRepository>();
        builder.Services.AddScoped<IDeviceInformationServiceMock, DeviceRepositoryMock>();

        // ACCOUNT
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
        builder.Services.AddScoped<AccountService>();
        builder.Services.AddScoped<EmailService>();
        builder.Services.AddScoped<ProfileService>();
        #endregion DEPENDENCY INJECTIONS

        builder.Services.AddControllers();
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
        // it will actually create a database if it does not exist in SQLite
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            // in order to use await in a method, the caller method must be async as well
            await CommonSeedData.Seed(context);
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