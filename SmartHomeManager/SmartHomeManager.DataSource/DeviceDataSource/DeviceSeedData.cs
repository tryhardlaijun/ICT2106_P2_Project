using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.DataSource.DeviceDataSource
{
    public static class DeviceSeedData
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            if (context.Accounts.Any()) return;
            if (context.Profiles.Any()) return;
            if (context.DeviceTypes.Any()) return;
            if (context.Devices.Any()) return;

            context.Devices.RemoveRange(context.Devices);
            context.Profiles.RemoveRange(context.Profiles);
            context.DeviceTypes.RemoveRange(context.DeviceTypes);
            context.Accounts.RemoveRange(context.Accounts);
            await context.SaveChangesAsync();

            var accounts = new List<Account>
            {
                new Account
                {
                    AccountId = new("06419047-3d8e-47fa-a239-80b7f78c4a2e"),
                    Email = "John",
                    Username = "Doe",
                    Address = "Ang Mo Kio",
                    Timezone = 8,
                    Password = "Ang Mo Kio",
                }
            };

            var profiles = new List<Profile>
            {
                new Profile
                {
                    ProfileId = new("5aa787d2-6f13-4a60-9894-2d9cab41bd6b"),
                    Name = "My Profile",
                    AccountId = accounts[0].AccountId,
                }
            };

            var deviceTypes = new List<DeviceType>
            {
                new DeviceType
                {
                    DeviceTypeName = "Light",
                }
            };

            await context.Accounts.AddRangeAsync(accounts);
            await context.SaveChangesAsync();

            await context.Profiles.AddRangeAsync(profiles);
            await context.SaveChangesAsync();

            await context.DeviceTypes.AddRangeAsync(deviceTypes);
            await context.SaveChangesAsync();
        }
    }
}
