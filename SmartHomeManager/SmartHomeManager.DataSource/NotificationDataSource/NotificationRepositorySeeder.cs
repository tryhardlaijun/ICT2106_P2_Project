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
            context.Accounts.RemoveRange(context.Accounts);
            await context.SaveChangesAsync();

            context.Notifications.RemoveRange(context.Notifications);
            await context.SaveChangesAsync();

            Guid TempAccountGuid = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709");

            Account addAccount = new Account
            {
                AccountId = TempAccountGuid,
                Email = "test@email.com",
                Username = "test123",
                Address = "Singapore 000000",
                Timezone = 8,
                Password = "test123password"
            };


            // Add Account...
            await context.Accounts.AddRangeAsync(addAccount);
            await context.SaveChangesAsync();

            for (int i = 0; i < AmountOfNotificationsToBeSeeded; i++)
            {
                Notification notification = new Notification
                {
                    AccountId = addAccount.AccountId,
                    NotificationMessage = i + " - test notification",
                    SentTime = DateTime.Now,
                    Account = addAccount
                };

                // Add to database...
                await context.Notifications.AddRangeAsync(notification);
            }

            await context.SaveChangesAsync();
        }
    }
}
