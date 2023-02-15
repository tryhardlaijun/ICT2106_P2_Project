using SmartHomeManager.DataSource.DeviceLogDataSource;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.NotificationDataSource
{
    public class NotificationRepositorySeeder
    {

        private const int AmountOfNotificationsToBeSeeded = 20;
        
        public static async Task Seed(ApplicationDbContext context)
        {

            System.Diagnostics.Debug.WriteLine("Ran the seeder...!\n\n\n");

            //if (context.Notifications.Any()) return;
            //if (context.Accounts.Any()) return;

            // Only works for <1000 rows...
            //context.Accounts.RemoveRange(context.Accounts);
            //await context.SaveChangesAsync();

            context.Notifications.RemoveRange(context.Notifications);
            await context.SaveChangesAsync();

            Guid TempAccountGuid = DeviceLogSeedData.TempAccountGuid;
            
            // Account to add notifications to...
            Account? account = await context.Accounts.FindAsync(TempAccountGuid);

            for (int i = 0; i < AmountOfNotificationsToBeSeeded; i++)
            {
                Notification notification = new Notification
                {
                    AccountId = account.AccountId,
                    NotificationMessage = i + " - test notification",
                    SentTime = DateTime.Now,
                    Account = account
                };

                // Add to database...
                await context.Notifications.AddRangeAsync(notification);
            }

            await context.SaveChangesAsync();
        }
    }
}
