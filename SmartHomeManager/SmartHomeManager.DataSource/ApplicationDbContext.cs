using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.DataSource
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; private set; }
        public DbSet<Device> Devices { get; private set; }
        public DbSet<DeviceConfiguration> DeviceConfigurations { get; private set; }
        public DbSet<DeviceType> DeviceTypes { get; private set; }
        public DbSet<DeviceConfigurationLookUp> DeviceConfigurationLookUps { get; private set; }
        public DbSet<DeviceLog> DeviceLogs { get; private set; }
        public DbSet<Notification> Notifications { get; private set; }
        public DbSet<Rule> Rules { get; private set; }
        public DbSet<Scenario> Scenarios { get; private set; }
        public DbSet<Troubleshooter> Troubleshooters { get; private set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Rooms = base.Set<Room>();
            Devices = base.Set<Device>();
            DeviceConfigurations = base.Set<DeviceConfiguration>();
            DeviceTypes = base.Set<DeviceType>();
            DeviceConfigurationLookUps = base.Set<DeviceConfigurationLookUp>();
            DeviceLogs = base.Set<DeviceLog>();
            Notifications = base.Set<Notification>();
            Rules = base.Set<Rule>();
            Scenarios = base.Set<Scenario>();
            Troubleshooters = base.Set<Troubleshooter>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeviceConfigurationLookUp>().HasKey(c => new { c.ConfigurationKey, c.DeviceBrand, c.DeviceModel });
            modelBuilder.Entity<DeviceConfiguration>().HasKey(c => new { c.ConfigurationKey, c.DeviceId });
        }

    }
}
