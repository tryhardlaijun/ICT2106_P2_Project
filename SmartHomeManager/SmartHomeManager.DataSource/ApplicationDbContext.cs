using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceStoreDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.DataSource;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        Accounts = base.Set<Account>();
        DeviceConfigurationLookUps = base.Set<DeviceConfigurationLookUp>();
        DeviceConfigurations = base.Set<DeviceConfiguration>();
        DeviceCoordinates = base.Set<DeviceCoordinate>();
        DeviceLogs = base.Set<DeviceLog>();
        DeviceTypes = base.Set<DeviceType>();
        DeviceProfiles = base.Set<DeviceProfile>();
        Devices = base.Set<Device>();
        Notifications = base.Set<Notification>();
        Profiles = base.Set<Profile>();
        RoomCoordinates = base.Set<RoomCoordinate>();
        Rooms = base.Set<Room>();
        Rules = base.Set<Rule>();
        Histories = base.Set<History>();
        RuleHistories = base.Set<RuleHistory>();
        Scenarios = base.Set<Scenario>();
        Troubleshooters = base.Set<Troubleshooter>();
        DeviceProducts = base.Set<DeviceProduct>();
    }

    public DbSet<Account> Accounts { get; }
    public DbSet<DeviceConfigurationLookUp> DeviceConfigurationLookUps { get; }
    public DbSet<DeviceConfiguration> DeviceConfigurations { get; }
    public DbSet<DeviceCoordinate> DeviceCoordinates { get; }
    public DbSet<DeviceLog> DeviceLogs { get; }
    public DbSet<DeviceType> DeviceTypes { get; }
    public DbSet<Device> Devices { get; }
    public DbSet<DeviceProfile> DeviceProfiles { get; }
    public DbSet<Notification> Notifications { get; }
    public DbSet<Profile> Profiles { get; }
    public DbSet<RoomCoordinate> RoomCoordinates { get; }
    public DbSet<Room> Rooms { get; }
    public DbSet<DeviceProduct> DeviceProducts { get; }
    public DbSet<Rule> Rules { get; }
    public DbSet<History> Histories { get; }
    public DbSet<RuleHistory> RuleHistories { get; }
    public DbSet<Scenario> Scenarios { get; }
    public DbSet<Troubleshooter> Troubleshooters { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DeviceConfigurationLookUp>()
            .HasKey(c => new { c.ConfigurationKey, c.DeviceBrand, c.DeviceModel });
        modelBuilder.Entity<DeviceConfiguration>().HasKey(c => new { c.ConfigurationKey, c.DeviceId });

        // Remove on delete cascade, make RoomId nullable on Device table...
        modelBuilder.Entity<Room>()
            .HasOne(room => room.Device)
            .WithOne(device => device.Room)
            .HasForeignKey<Device>(device => device.RoomId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<DeviceProfile>().HasKey(deviceProfile => new { deviceProfile.DeviceId, deviceProfile.ProfileId });
    }
}