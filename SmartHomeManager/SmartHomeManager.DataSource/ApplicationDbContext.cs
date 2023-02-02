using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.DataSource
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; private set; } 
        public DbSet<DeviceConfigurationLookUp> DeviceConfigurationLookUps { get; private set; }
        public DbSet<DeviceConfiguration> DeviceConfigurations { get; private set; }
        public DbSet<DeviceCoordinate> DeviceCoordinates { get; private set; }
        public DbSet<DeviceLog> DeviceLogs { get; private set; }
        public DbSet<DeviceType> DeviceTypes { get; private set; }
        public DbSet<Device> Devices { get; private set; }
        public DbSet<Notification> Notifications { get; private set; }
        public DbSet<Profile> Profiles { get; private set; }
        public DbSet<RoomCoordinate> RoomCoordinates { get; private set; }
        public DbSet<Room> Rooms { get; private set; }
        public DbSet<Rule> Rules { get; private set; }
        public DbSet<Scenario> Scenarios { get; private set; }
        public DbSet<Troubleshooter> Troubleshooters { get; private set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            Accounts = base.Set<Account>();
            DeviceConfigurationLookUps = base.Set<DeviceConfigurationLookUp>();
            DeviceConfigurations = base.Set<DeviceConfiguration>();
            DeviceCoordinates = base.Set<DeviceCoordinate>();
            DeviceLogs = base.Set<DeviceLog>();
            DeviceTypes = base.Set<DeviceType>();
            Devices = base.Set<Device>();
            Notifications = base.Set<Notification>();
            Profiles = base.Set<Profile>();
            RoomCoordinates= base.Set<RoomCoordinate>();
            Rooms = base.Set<Room>();
            Rules = base.Set<Rule>();
            Scenarios = base.Set<Scenario>();
            Troubleshooters = base.Set<Troubleshooter>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeviceConfigurationLookUp>().HasKey(c => new { c.ConfigurationKey, c.DeviceBrand, c.DeviceModel });
            modelBuilder.Entity<DeviceConfiguration>().HasKey(c => new { c.ConfigurationKey, c.DeviceId });

            // Remove on delete cascade, make RoomId nullable on Device table...
            modelBuilder.Entity<Room>()
                .HasOne(room => room.Device)
                .WithOne(device => device.Room)
                .HasForeignKey<Device>(device => device.RoomId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
