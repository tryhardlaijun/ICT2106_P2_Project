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
        public static async Task Seed(ApplicationDbContext context)
        {
            if (context.Notifications.Any()) return;
            if (context.Accounts.Any()) return;

            context.Accounts.RemoveRange(context.Accounts);
            await context.SaveChangesAsync();

            context.Notifications.RemoveRange(context.Notifications);
            await context.SaveChangesAsync();



            Account addAccount = new Account
            {
                AccountId = Guid.NewGuid(),
                Email = "test@email.com",
                Username = "test123",
                Address = "Singapore 000000",
                Timezone = 8,
                Password = "test123password"
            };

           
            Notification notificationToBeAdded = new Notification
            {
                AccountId = addAccount.AccountId,
                NotificationMessage = "test",
                SentTime = DateTime.Now,
                Account = addAccount
            };





        }
    }
}
