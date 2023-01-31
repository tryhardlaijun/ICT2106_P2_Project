using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource.DeviceDataSource;
using SmartHomeManager.DataSource.DeviceDataSource.Services;
using SmartHomeManager.DataSource.SampleDataSource;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Services;
using SmartHomeManager.Domain.SampleDomain.Entities;

namespace SmartHomeManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            #region DEPENDENCY INJECTIONS
            #region SAMPLE
            builder.Services.AddDbContext<SampleDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IGenericRepository<Sample>, SampleRepository>();
            #endregion SAMPLE
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

            app.Run();
        }
    }
}