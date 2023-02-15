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

            #region DEPENDENCY INJECTIONS
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IGenericRepository<History>, DataSource.HistoryDataSource.HistoryRepository>();
            builder.Services.AddScoped<IRuleHistoryRepository<RuleHistory>, RuleHistoryRepository>();
            builder.Services.AddScoped<IGenericRepository<Rule>, RuleRepository>();
            builder.Services.AddScoped<IGenericRepository<Profile>, ProfileRepository>();
            builder.Services.AddScoped<IGenericRepository<EnergyProfile>, EnergyProfileRepository>();
            builder.Services.AddScoped<IGenericRepository<Scenario>, ScenarioRepository>();
            builder.Services.AddScoped<IGetRulesService, GetRulesServices>();
            builder.Services.AddScoped<IGetScenariosService, GetScenariosService>();
            builder.Services.AddScoped<IInformDirectorServices, DirectorServices>();
            builder.Services.AddScoped<IEnergyProfileServices, EnergyProfileServices>();
            builder.Services.AddScoped<IGenericRepository<HomeSecurity>, HomeSecurityRepository>();
            builder.Services.AddScoped<IGenericRepository<HomeSecuritySetting>, HomeSecuritySettingRepository>();
            builder.Services.AddScoped<IHomeSecurityDeviceDefinitionRepository<HomeSecurityDeviceDefinition>, HomeSecurityDeviceDefinitionRepository>();
            #endregion DEPENDENCY INJECTIONS
        // DEVICE
        builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
        builder.Services.AddScoped<IDeviceTypeRepository, DeviceTypeRepository>();

        // ROOM
        builder.Services.AddScoped<IRoomRepository, RoomRepository>();
        builder.Services.AddScoped<IDeviceInformationServiceMock, DeviceRepositoryMock>();

            builder.Services.AddHostedService<DirectorServices>();

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

        app.Run();
    }


}