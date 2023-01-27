
using SmartHomeManager.DataSource.Repositories;
using SmartHomeManager.DataSource.Services;
using SmartHomeManager.Domain.Entities;
using SmartHomeManager.Domain.Interfaces;
using SmartHomeManager.Domain.Interfaces.Services;

namespace SmartHomeManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            #region DEPENDENCY INJECTION

            builder.Services.AddScoped<IGenericRepository<Device>, DeviceRepository>();
            builder.Services.AddScoped<IDeviceService, DeviceService>();
            #endregion

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

            app.Run();
        }
    }
}