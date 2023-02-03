using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.DataSource
{
    public class ProfileSeedData
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            // If there is data, don't do anything
            if (context.Profiles.Any()) return;

            // create objects
            var profiles = new List<Profile>
            {
                new Profile()
                {
                    ProfileId = Guid.NewGuid(),
                    AccountId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Default Profile",
                    Account = new Account()
                    {
                        AccountId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        Email = "abc@xyz.com",
                        Username = "username",
                        Password = "password",
                        Address = "123 abc 456 888888",
                        Timezone = 8
                    }
                }
            };

            // add to repository and commit those changes
            await context.Profiles.AddRangeAsync(profiles);
            await context.SaveChangesAsync();
        }
    }
}
