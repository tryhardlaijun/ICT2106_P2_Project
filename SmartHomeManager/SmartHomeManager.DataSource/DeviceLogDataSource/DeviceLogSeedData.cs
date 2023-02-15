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

        public static Guid TempAccountGuid = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709");
        public static Guid TempProfileGuid = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857710");
        public static Guid TempDeviceGuid = new Guid("385576BC-F97B-4B95-9B40-F423E4D16623");

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
            context.Accounts.RemoveRange(context.Accounts);
            //await context.SaveChangesAsync();

            context.Profiles.RemoveRange(context.Profiles);
            context.DeviceTypes.RemoveRange(context.DeviceTypes);
            context.Devices.RemoveRange(context.Devices);
            context.DeviceLogs.RemoveRange(context.DeviceLogs);

            await context.SaveChangesAsync();

            // Device to be seeded....
            Device deviceToBeSeeded;

            var accounts = new List<Account>
            {
                new()
                {
                    AccountId = TempAccountGuid,
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
                    ProfileId = TempProfileGuid,
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


            deviceToBeSeeded = new()
            {
                DeviceId = TempDeviceGuid,
                DeviceName = "Light",
                DeviceBrand = "Philips",
                DeviceModel = "Hue",
                DeviceTypeName = deviceTypes[0].DeviceTypeName,
                AccountId = accounts[0].AccountId,
                ProfileId = profiles[0].ProfileId
            };

            Random rnd = new Random();
            var DeviceLog = new List<DeviceLog>();
            for (int j = 13; j < 13+7; j++) { 
                // create objects
                for (int i = 0; i < 23; i++)
                {
                    DeviceLog.Add(new DeviceLog()
                    {
                        LogId = Guid.NewGuid(),
                        StartTime = DateTime.Parse($"2023-02-{j} {i}:00:00.0000000"),
                        EndTime = DateTime.Parse($"2023-02-{j} {i + 1}:00:00.0000000"),
                        DateLogged = DateTime.Parse($"2023-02-{j} 00:00:00.0000000"),
                        DeviceEnergyUsage = rnd.Next(100, 1000),
                        DeviceActivity = 1,
                        DeviceState = false,
                        DeviceId = deviceToBeSeeded.DeviceId,
                        Device = deviceToBeSeeded
                    });
                }
            }

            // add to repository and commit those changes
            await context.Accounts.AddRangeAsync(accounts);
            await context.SaveChangesAsync();
            await context.Profiles.AddRangeAsync(profiles);
            await context.SaveChangesAsync();

            await context.DeviceTypes.AddRangeAsync(deviceTypes);
            await context.SaveChangesAsync();

            await context.Devices.AddRangeAsync(deviceToBeSeeded);
            await context.SaveChangesAsync();

            await context.DeviceLogs.AddRangeAsync(DeviceLog);
            await context.SaveChangesAsync();
        }
    }
}
