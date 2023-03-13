using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.EnergyProfileDomain.Interfaces;
using SmartHomeManager.Domain.EnergyProfileDomain.Services;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using System.Data;
using System.Text;
using Rule = SmartHomeManager.Domain.SceneDomain.Entities.Rule;

namespace SmartHomeManager.Domain.DirectorDomain.Services
{
    public class DirectorServices : BackgroundService, IInformDirectorServices
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IGetRulesService _ruleInterface;
        private readonly IGetScenariosService _scenarioInterface;
        private readonly IEnergyProfileServices _energyProfileInterface;

        private readonly IRuleHistoryRepository<RuleHistory> _ruleHistoryRepository;
        private readonly IGenericRepository<History> _historyRepository;

        private List<Rule> rules;
        private List<Scenario> scenarios;
        private DateTime timeMark;

        public DirectorServices(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _ruleHistoryRepository = scope.ServiceProvider.GetRequiredService<IRuleHistoryRepository<RuleHistory>>();
            _historyRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<History>>();

            _ruleInterface = scope.ServiceProvider.GetRequiredService<IGetRulesService>();
            _scenarioInterface = scope.ServiceProvider.GetRequiredService<IGetScenariosService>();
            _energyProfileInterface = scope.ServiceProvider.GetRequiredService<IEnergyProfileServices>();

            rules = new List<Rule>();
            scenarios= new List<Scenario>();
            timeMark = DateTime.Now.AddMinutes(-1);
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {            
            rules = (await _ruleInterface.GetAllRules()).ToList();       
            scenarios = (await _scenarioInterface.GetAllScenarios()).ToList();

            while (!stoppingToken.IsCancellationRequested)
            {             
                if(TimeCheck()) CheckIfRuleTriggered();
                await Task.Delay(10000);
            }
        }

        private bool TimeCheck()
        {
            var now = DateTime.Now;
            var timediff = Math.Floor((now - timeMark).TotalMinutes);
            if (timediff > 0) {
                timeMark = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
                return true;
            }
            return false;
        }

        private async void CheckIfRuleTriggered()
        {
            Console.WriteLine(string.Format("System Time: {0}", DateTime.Now.ToString("HH:mm:ss.fff")));

            if (rules.Any())
            {
                var rLength = rules.Count();
                foreach (var rule in rules)
                {
                    var timeDiff = Math.Floor((DateTime.Now - Convert.ToDateTime(rule.StartTime)).TotalMinutes);
                    if (timeDiff == 0)
                    {
                        Console.WriteLine("Trigger Detected: " + rule.RuleName);
                        var deviceID = rule.DeviceId;
                        var configKey = rule.ConfigurationKey;
                        var configValue = rule.ConfigurationValue;

                        int adjustedConfigValue = await _energyProfileInterface.getRevisedConfigValue(deviceID, configKey, configValue);

                        // Set device thru device interface
                        // await setDeviceConfig(deviceID, configKey, configValue);

                        var configMeaning = string.Format("{0} is set to {1}", configKey, configValue); // await getConfigMeaning(deviceID, configKey, configValue);
                        var storedRule = await _ruleHistoryRepository.GetByRuleIdAsync(rule.RuleId);

                        History h = new History();
                        h.Message = configMeaning;
                        h.Timestamp = DateTime.Now;
                        h.DeviceAdjustedConfiguration = adjustedConfigValue;
                        h.ProfileId = rule.Scenario.ProfileId;
                        h.RuleHistoryId = storedRule.RuleHistoryId;

                        await _historyRepository.AddAsync(h);

                        // Notify troubleshooter interface
                        // await informTrigger(deviceID, configKey, configValue);
                    }
                }
            }            
        }

        public async void InformRuleChangesAsync(Guid ruleID, char operation)
        {
            switch (operation)
            {
                case 'c':                    
                    await addNewRule(await _ruleInterface.GetRuleById(ruleID));                    
                    break;
                case 'u':
                    rules = rules.Where(r => r.RuleId != ruleID).ToList();
                    await addNewRule(await _ruleInterface.GetRuleById(ruleID));                                     
                    break;
                case 'd':
                    rules = rules.Where(r => r.RuleId != ruleID).ToList();
                    break;
            }

            async Task addNewRule(Rule rule){                
                rules.Add(rule);
                RuleHistory rh = new RuleHistory
                {
                    RuleId = ruleID,
                    RuleIndex = await _ruleHistoryRepository.CountRuleAsync(),
                    RuleName = rule.RuleName,
                    RuleStartTime = rule.StartTime,
                    RuleEndTime = rule.EndTime,
                    RuleActionTrigger = rule.ActionTrigger,
                    ScenarioName = rule.Scenario.ScenarioName,
                    DeviceName = rule.Device.DeviceName,
                    DeviceConfiguration = string.Format("{0} is triggered", rule.Device.DeviceName) // await getConfigMeaning(rule.Device.DeviceId, rule.ConfigurationKey, rule.ConfigurationValue);
                };
                await _ruleHistoryRepository.AddAsync(rh);
            }
        }

        public async void InformScenarioChangesAsync(Guid scenarioID, char operation)
        {
            switch (operation)
            {
                case 'c':                    
                    scenarios.Add(await _scenarioInterface.GetScenarioById(scenarioID));                    
                    break;
                case 'u':                    
                    scenarios = scenarios.Where(s => s.ScenarioId != scenarioID).ToList();
                    scenarios.Add(await _scenarioInterface.GetScenarioById(scenarioID));                                     
                    break;
                case 'd':
                    scenarios = scenarios.Where(s => s.ScenarioId != scenarioID).ToList();
                    rules = rules.Where(r => r.ScenarioId != scenarioID).ToList();            
                    break;
            }
        }
    }
}
