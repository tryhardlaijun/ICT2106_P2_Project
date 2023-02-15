using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.DataSource.ProfileDataSource
{
    public class DeviceProfilesSeedData
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            // If there is data, don't do anything
            if (context.DeviceProfiles.Any()) return;
            if (context.Devices.Any()) return;
            if (context.DeviceTypes.Any()) return;

            //var test = context.DeviceProfile.Find("B1FB4CAC-51B0-43F4-B784-A264E89C2696");
            //Console.WriteLine(test);

            var deviceTypes = new List<DeviceType>
            {
                new DeviceType
                {
                    DeviceTypeName = "Light",
                }
            };

            var devices = new List<Device>
            {
                new Device
                {
                    DeviceId = new("33333333-3333-3333-3333-333333333333"),
                    DeviceName = "name",
                    DeviceBrand = "xiaomi",
                    DeviceModel = "smart fan",
                    DeviceWatts = 100,
                    DeviceTypeName = "Light",
                    DeviceSerialNumber = "1234",
                    AccountId = new ("11111111-1111-1111-1111-111111111111")
                }
            };

            // create objects
            var deviceProfiles = new List<DeviceProfile>
            {
                new DeviceProfile()
                {
                    DeviceId = devices[0].DeviceId,
                    ProfileId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                }
            };

            // add to repository and commit those changes
            await context.DeviceTypes.AddRangeAsync(deviceTypes);
            await context.Devices.AddRangeAsync(devices);
            await context.DeviceProfiles.AddRangeAsync(deviceProfiles);
            
            await context.SaveChangesAsync();
        }
    }
}
