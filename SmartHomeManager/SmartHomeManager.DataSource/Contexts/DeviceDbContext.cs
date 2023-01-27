using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Entities;

namespace SmartHomeManager.DataSource.Contexts
{
    public class DeviceDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }

        public DeviceDbContext(DbContextOptions<DeviceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Device>().Property(device => device.Id).ValueGeneratedOnAdd();
        }
    }
}
