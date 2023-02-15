using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource
{
    public class CommonSeedData
    {
        public static async Task Seed(ApplicationDbContext context)
        {
            // If there is data, don't do anything
            if (context.Accounts.Any()) return;
            if (context.Profiles.Any()) return;
            if (context.DeviceProfiles.Any()) return;
            if (context.Devices.Any()) return;
            if (context.DeviceTypes.Any()) return;

            // create objects
            var accounts = new List<Account>
            {
                new Account()
                {
                    AccountId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Email = "abc@xyz.com",
                    Username = "username",
                    Password = "P@ssw0rd",
                    Address = "123 abc 456 888888",
                    Timezone = 8
                }
            };

            // add to repository and commit those changes
            await context.Accounts.AddRangeAsync(accounts);
            await context.SaveChangesAsync();

            // create objects
            var profiles = new List<Profile>
            {
                new Profile()
                {
                    ProfileId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    AccountId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Default Profile"
                }
            };

            // add to repository and commit those changes
            await context.Profiles.AddRangeAsync(profiles);
            await context.SaveChangesAsync();

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

            var rooms = new List<Room>
            {
                new()
                {
                    RoomId = Guid.NewGuid(),
                    Name = "Bedroom",
                    AccountId = accounts[0].AccountId
                }
            };

            var roomCoordinates = new List<RoomCoordinate>
            {
                new()
                {
                    XCoordinate = 0,
                    YCoordinate = 0,
                    Width = 2,
                    Height = 1,
                    RoomId = rooms[0].RoomId
                }
            };

            var deviceCoordinates = new List<DeviceCoordinate>
            {
                new()
                {
                    XCoordinate = 0,
                    YCoordinate = 0,
                    Width = 2,
                    Height = 1,
                    DeviceId = devices[0].DeviceId
                }
            };

            await context.Rooms.AddRangeAsync(rooms);
            await context.SaveChangesAsync();

            await context.RoomCoordinates.AddRangeAsync(roomCoordinates);
            await context.SaveChangesAsync();

            await context.DeviceCoordinates.AddRangeAsync(deviceCoordinates);
            await context.SaveChangesAsync();

            var scenarios = new List<Scenario>
            {
                new Scenario
                {
                    ScenarioId = new("AC38AF14-9A57-4DF3-89F3-78F9CE9F4983"),
                    ScenarioName = "string",
                    RuleList = "string",
                    ProfileId = profiles[0].ProfileId
                }
            };

            await context.Scenarios.AddRangeAsync(scenarios);
            await context.SaveChangesAsync();

            var rules = new List<Rule>
            {
                new Rule
                {
                    RuleId = Guid.NewGuid(),
                    ScenarioId = scenarios[0].ScenarioId,
                    ConfigurationValue = 0,
                    ActionTrigger = "string",
                    RuleName = "string",
                    StartTime = Convert.ToDateTime("2023-02-04T07:21:26.934Z"),
                    EndTime = Convert.ToDateTime("2023-02-04T07:21:26.934Z"),
                    DeviceId = devices[0].DeviceId
                }                
            };

            await context.Rules.AddRangeAsync(rules);
            await context.SaveChangesAsync();

            var ruleHistory = new List<RuleHistory>
            {
                new RuleHistory
                {            
                    RuleHistoryId = Guid.NewGuid(),
                    RuleId = rules[0].RuleId,
                    RuleIndex = 0,
                    RuleName = rules[0].RuleName,
                    RuleStartTime = rules[0].StartTime,
                    RuleEndTime = rules[0].EndTime,
                    RuleActionTrigger = rules[0].ActionTrigger,
                    ScenarioName = rules[0].Scenario.ScenarioName,
                    DeviceName = rules[0].Device.DeviceName,
                    DeviceConfiguration = string.Format("{0} triggering ...", rules[0].Device.DeviceName)
                }
            };

            await context.RuleHistories.AddRangeAsync(ruleHistory);
            await context.SaveChangesAsync();

            var history = new List<History>
            {
                new History
                {
                    Message = string.Format("{0} is triggered", rules[0].Device.DeviceName),
                    Timestamp = Convert.ToDateTime("2023-02-04T07:21:26.934Z"),
                    DeviceAdjustedConfiguration = 1,
                    ProfileId = profiles[0].ProfileId,
                    RuleHistoryId = ruleHistory[0].RuleHistoryId
        }
            };

            await context.Histories.AddRangeAsync(history);
            await context.SaveChangesAsync();
        }
    }
}
