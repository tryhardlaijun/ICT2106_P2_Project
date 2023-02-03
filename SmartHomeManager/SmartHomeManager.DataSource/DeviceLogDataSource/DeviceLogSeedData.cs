using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.DeviceLogDataSource
{
    public class DeviceLogSeedData
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            // If there is data, don't do anything
            if (context.DeviceLogs.Any()) return;

​
        // create objects
        var DeviceLog = new List<DeviceLog>
            {
                new DeviceLog()
                {
                    LogId = Guid.NewGuid(),
                    StartTime = DateTime.Now,
                    EndTime = null,
                    DateLogged = DateTime.Now,
                    DeviceEnergyUsage = 0,
                    DeviceActivity = 0,
                    DeviceState = true,
                    DeviceId = Guid.NewGuid()
                }
            };
​
        
​
        // add to repository and commit those changes
            await context.DeviceLogs.AddRangeAsync(DeviceLog);
            await context.SaveChangesAsync();
        }
    }
}
