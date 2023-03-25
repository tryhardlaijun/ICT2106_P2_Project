using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartHomeManager.Domain.APIDomain.Interface;
using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.BackupDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.EnergyProfileDomain.Interfaces;
using SmartHomeManager.Domain.EnergyProfileDomain.Services;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using System;
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
        private readonly IBackupService _backupInterface;
        private readonly IAPIDataService _apiInterface;

        private readonly IRuleHistoryRepository<RuleHistory> _ruleHistoryRepository;
        private readonly IGenericRepository<History> _historyRepository;

        private RuleList? ruleList;
        private ScenarioList? scenarioList;

        private DateTime timeMark;

        private RuleTriggerManager ruleTriggerManager;

        private IDirectorControlDeviceService _directorControlDeviceInterface;
        private ITroubleshooterServices _troubleshooterInterface;

        public DirectorServices(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _ruleHistoryRepository = scope.ServiceProvider.GetRequiredService<IRuleHistoryRepository<RuleHistory>>();
            _historyRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<History>>();

            _ruleInterface = scope.ServiceProvider.GetRequiredService<IGetRulesService>();
            _scenarioInterface = scope.ServiceProvider.GetRequiredService<IGetScenariosService>();
            _energyProfileInterface = scope.ServiceProvider.GetRequiredService<IEnergyProfileServices>();
            _backupInterface = scope.ServiceProvider.GetRequiredService<IBackupService>();
            _directorControlDeviceInterface = scope.ServiceProvider.GetRequiredService<IDirectorControlDeviceService>();
            _troubleshooterInterface = scope.ServiceProvider.GetRequiredService<ITroubleshooterServices>();
            _apiInterface = scope.ServiceProvider.GetRequiredService<IAPIDataService>();

            timeMark = DateTime.Now.AddMinutes(-1);

            ruleTriggerManager = new RuleTriggerManager();
            ruleTriggerManager.Attach(_directorControlDeviceInterface);
            ruleTriggerManager.Attach(_troubleshooterInterface);

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            scenarioList = new ScenarioList((await _scenarioInterface.GetAllScenarios()).ToList());
            ruleList = new RuleList((await _ruleInterface.GetAllRules()).ToList());

            //_backupInterface.createBackup(rules, scenarios);

            await Task.Delay(1000);

            while (!stoppingToken.IsCancellationRequested)
            {
                //if (TimeCheck()) CheckIfRuleTriggered();
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
            var ruleListClone = ruleList!.Clone().getRuleList();
            var apiData = await _apiInterface.getAPIData();

            if (ruleListClone.Any())
            {
                var rLength = ruleListClone.Count();
                foreach (var rule in ruleListClone)
                {
                    if (rule.StartTime != null)
                    {
                        var now = DateTime.Now.ToString("HH:mm");
                        var startTime = rule.StartTime?.ToString("HH:mm");
                        if (!now.Equals(startTime)) continue;
                    }

                    if (rule.APIKey != null) 
                    {
                        string value;
                        if (!apiData.TryGetValue(rule.APIKey, out value)) continue;
                        if (value != rule.ApiValue) continue;
                    }

                    if (rule.ActionTrigger != null)
                    {
                        Random random = new Random();
                        if (random.Next(2) == 0) return;
                    }


                    Console.WriteLine("Trigger Detected: " + rule.RuleName);
                    var deviceID = rule.DeviceId;
                    var configKey = rule.ConfigurationKey;
                    var configValue = rule.ConfigurationValue;

                    int adjustedConfigValue = await _energyProfileInterface.getRevisedConfigValue(deviceID, configKey, configValue);

                    // Set device thru device interface
                    ruleTriggerManager.Notify(deviceID, configKey, adjustedConfigValue);

                    var configMeaning = string.Format("{0} is set to {1}", configKey, configValue); // await getConfigMeaning(deviceID, configKey, configValue);
                    var storedRule = await _ruleHistoryRepository.GetByRuleIdAsync(rule.RuleId);

                    History h = new History();
                    h.Message = configMeaning;
                    h.Timestamp = DateTime.Now;
                    h.DeviceAdjustedConfiguration = adjustedConfigValue;
                    h.ProfileId = rule.Scenario.ProfileId;
                    h.RuleHistoryId = storedRule.RuleHistoryId;

                    await _historyRepository.AddAsync(h);


                }
            }            
        }

        public async void InformRuleChangesAsync(Guid ruleID, char operation)
        {
            var ruleListClone = ruleList!.Clone().getRuleList();
            switch (operation)
            {
                case 'c':
                    await addNewRule(await _ruleInterface.GetRuleById(ruleID));
                    break;
                case 'u':
                    ruleListClone = ruleListClone.Where(r => r.RuleId != ruleID).ToList();
                    await addNewRule(await _ruleInterface.GetRuleById(ruleID));
                    break;
                case 'd':
                    ruleListClone = ruleListClone.Where(r => r.RuleId != ruleID).ToList();
                    break;
            }

            async Task addNewRule(Rule rule)
            {
                ruleListClone.Add(rule);
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

            ruleList.replaceRuleList(ruleListClone);
        }

        public async void InformScenarioChangesAsync(Guid scenarioID, char operation)
        {
            var scenarioListClone = scenarioList!.Clone().getScenarioList();
            switch (operation)
            {
                case 'c':
                    scenarioListClone.Add(await _scenarioInterface.GetScenarioById(scenarioID));
                    break;
                case 'u':
                    scenarioListClone = scenarioListClone.Where(s => s.ScenarioId != scenarioID).ToList();
                    scenarioListClone.Add(await _scenarioInterface.GetScenarioById(scenarioID));
                    break;
                case 'd':
                    scenarioListClone = scenarioListClone.Where(s => s.ScenarioId != scenarioID).ToList();

                    var ruleListClone = ruleList!.Clone().getRuleList();
                    ruleListClone = ruleListClone.Where(r => r.ScenarioId != scenarioID).ToList();
                    ruleList.replaceRuleList(ruleListClone);
                    break;
            }
            scenarioList.replaceScenarioList(scenarioListClone);
        }

    }
}
