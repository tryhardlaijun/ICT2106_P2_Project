using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartHomeManager.DataSource.RuleHistoryDataSource;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.DirectorDomain.Services
{
    public class Director : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IEnumerable<Rule>? rules;
        private IEnumerable<Scenario>? scenarios;

        private IGenericRepository<Rule> _ruleRepository;
        private IRuleHistoryRepository<RuleHistory> _ruleHistoryRepository;
        private IGenericRepository<History> _historyRepository;

        public Director(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _ruleHistoryRepository = scope.ServiceProvider.GetRequiredService<IRuleHistoryRepository<RuleHistory>>();
            _historyRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<History>>();
            _ruleRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<Rule>>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            rules = await _ruleRepository.GetAllAsync();            

            while (!stoppingToken.IsCancellationRequested)
            {
                CheckIfRuleTriggered();
                await Task.Delay(30000);
            }
        }

        private async void CheckIfRuleTriggered()
        {
            Console.WriteLine(string.Format("{0} - {1}", "Director", DateTime.Now.ToString("HH:mm:ss.fff")));           
                        
            if (rules != null)
            {
                var rLength = rules.Count();
                foreach (var rule in rules)
                {
                    var timeDiff = Math.Floor((DateTime.Now - Convert.ToDateTime(rule.StartTime)).TotalMinutes);
                    Console.WriteLine(string.Format("Rule {0}: {1} {2}", rule.ScheduleName, rule.StartTime, timeDiff);
                    if (false || timeDiff == 0)
                    {
                        Console.WriteLine("Trigger Detected: " + rule.ScheduleName);
                        var deviceID = rule.DeviceId;
                        var configKey = "SPEED"; // rule.configurationValue
                        var configValue = 3; // rule.configurationKey

                        int adjustedConfigValue = 4;// getAdjustedConfigValue(deviceID, configKey, configValue);

                        // await setDeviceConfig(deviceID, configKey, configValue);

                        var configMeaning = string.Format("{0} is set to {1}", configKey, configValue); // await getConfigMeaning(deviceID, configKey, configValue);
                        var storedRule = await _ruleHistoryRepository.GetByRuleIdAsync(rule.RuleId);
                        Guid ruleHistoryId;
                        if (storedRule == null || !checkIfRuleHistoryMatch(rule, storedRule))
                        {                            
                            ruleHistoryId = Guid.NewGuid();

                            RuleHistory rh = new RuleHistory();
                            rh.RuleHistoryId = ruleHistoryId;
                            rh.RuleIndex = await _ruleHistoryRepository.GetRuleIndexLimitAsync();
                            rh.RuleName = rule.ScheduleName ?? "";
                            rh.RuleStartTime = rule.StartTime; 
                            rh.RuleEndTime = rule.EndTime;
                            // CONTINUE

                            await _ruleHistoryRepository.AddAsync(rh);
                        } else
                        {
                            ruleHistoryId = storedRule.RuleHistoryId;
                        }                        

                        History h = new History();
                        h.Message = configMeaning;
                        h.Timestamp = DateTime.Now.AddHours(-8);
                        h.DeviceAdjustedConfiguration = adjustedConfigValue;
                        h.ProfileId = rule.Device.ProfileId;
                        h.RuleHistoryId = ruleHistoryId;

                        await _historyRepository.AddAsync(h);

                        // await informTrigger(deviceID, configKey, configValue);
                    }
                }

            }
        }

        private bool checkIfRuleHistoryMatch(Rule r, RuleHistory ruleHistory)
        {

        }
    }
}
