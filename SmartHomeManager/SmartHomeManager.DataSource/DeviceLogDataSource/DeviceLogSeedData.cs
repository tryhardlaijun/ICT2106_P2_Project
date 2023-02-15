using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartHomeManager.DataSource.DeviceLogDataSource
{
    public class DeviceLogSeedData
    {
        public static async Task Seed(ApplicationDbContext context)
        {

            System.Diagnostics.Debug.WriteLine("test");
            // If there is data, don't do anything
            /*if (context.Accounts.Any()) return;
            if (context.Profiles.Any()) return;
            if (context.DeviceTypes.Any()) return;
            if (context.Devices.Any()) return;*/
            //if (context.DeviceLogs.Any()) return;

            // Delete all existing database objects for Room domain
            //context.Accounts.RemoveRange(context.Accounts);
            //await context.SaveChangesAsync();

      /*      context.Profiles.RemoveRange(context.Profiles);
            context.DeviceTypes.RemoveRange(context.DeviceTypes);
            context.Devices.RemoveRange(context.Devices);
            context.DeviceLogs.RemoveRange(context.DeviceLogs);
*/
           // await context.SaveChangesAsync();
/*

            var accounts = new List<Account>
        {
            new()
            {
                AccountId = Guid.NewGuid(),
                Email = "John",
                Username = "Doe",
                Address = "Ang Mo Kio",
                Timezone = 8,
                Password = "P@assw0rd"
            }
        };
            var profiles = new List<Profile>
        {
            new()
            {
                ProfileId = Guid.NewGuid(),
                Name = "My Profile",
                AccountId = accounts[0].AccountId
            }
        };

            var deviceTypes = new List<DeviceType>
        {
            new()
            {
                DeviceTypeName = "Light"
            }
        };

            var devices = new List<Device>
        {
            new()
            {
                DeviceId = Guid.NewGuid(),
                DeviceName = "Light",
                DeviceBrand = "Philips",
                DeviceModel = "Hue",
                DeviceTypeName = deviceTypes[0].DeviceTypeName,
                AccountId = accounts[0].AccountId,
                ProfileId = profiles[0].ProfileId
            }
        };*/

            Device? myDevice = await context.Devices.FindAsync(Guid.Parse("385576BC-F97B-4B95-9B40-F423E4D16623"));
            // create objects
            var DeviceLog = new List<DeviceLog>
            {
                new DeviceLog()
                {
                    LogId = Guid.NewGuid(),
                    StartTime = DateTime.Now,
                    EndTime = null,
                    DateLogged = DateTime.Now,
                    DeviceEnergyUsage = 6,
                    DeviceActivity = 6,
                    DeviceState = true,
                    DeviceId = myDevice.DeviceId,
                    Device = myDevice} };

            // add to repository and commit those changes
/*            await context.Accounts.AddRangeAsync(accounts);
            await context.SaveChangesAsync();
            await context.Profiles.AddRangeAsync(profiles);
            await context.SaveChangesAsync();

            await context.DeviceTypes.AddRangeAsync(deviceTypes);
            await context.SaveChangesAsync();

            await context.Devices.AddRangeAsync(devices);
            await context.SaveChangesAsync();
*/
            await context.DeviceLogs.AddRangeAsync(DeviceLog);
            await context.SaveChangesAsync();
        }
    }
}
