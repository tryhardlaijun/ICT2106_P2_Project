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
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using Rule = SmartHomeManager.Domain.SceneDomain.Entities.Rule;

namespace SmartHomeManager.Domain.DirectorDomain.Services
{
    public class DirectorServices : BackgroundService, IInformDirectorServices, IDirectorServices
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IGetRulesService _ruleInterface;
        private readonly IGetScenariosService _scenarioInterface;
        private readonly IEnergyProfileServices _energyProfileInterface;
        private readonly IBackupService _backupInterface;
        private readonly IAPIDataService _apiInterface;

        private readonly IRuleHistoryRepository _ruleHistoryRepository;
        private readonly IHistoryRepository _historyRepository;

        private RuleList? ruleList;
        private ScenarioList? scenarioList;

        private DateTime timeMark;
        private bool backUpFlag;

        private RuleTriggerManager ruleTriggerManager;

        private IDirectorControlDeviceService _directorControlDeviceInterface;
        private ITroubleshooterServices _troubleshooterInterface;

        public DirectorServices(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _ruleHistoryRepository = scope.ServiceProvider.GetRequiredService<IRuleHistoryRepository>();
            _historyRepository = scope.ServiceProvider.GetRequiredService<IHistoryRepository>();

            _ruleInterface = scope.ServiceProvider.GetRequiredService<IGetRulesService>();
            _scenarioInterface = scope.ServiceProvider.GetRequiredService<IGetScenariosService>();
            _energyProfileInterface = scope.ServiceProvider.GetRequiredService<IEnergyProfileServices>();
            _backupInterface = scope.ServiceProvider.GetRequiredService<IBackupService>();
            _directorControlDeviceInterface = scope.ServiceProvider.GetRequiredService<IDirectorControlDeviceService>();
            _troubleshooterInterface = scope.ServiceProvider.GetRequiredService<ITroubleshooterServices>();
            _apiInterface = scope.ServiceProvider.GetRequiredService<IAPIDataService>();

            timeMark = DateTime.Now.AddMinutes(-1);
            backUpFlag = false;

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
                if (TriggerTimeCheck()) CheckIfRuleTriggered();
                if (BackupTimeCheck()) _backupInterface.createBackup(ruleList!.Clone().getRuleList(), scenarioList!.Clone().getScenarioList());
                await Task.Delay(10000);
            }
        }

        private bool TriggerTimeCheck()
        {
            var now = DateTime.Now;
            var timediff = Math.Floor((now - timeMark).TotalMinutes);
            if (timediff > 0)
            {
                timeMark = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
                return true;
            }
            return false;
        }

        private bool BackupTimeCheck()
        {
            var now = Int16.Parse(DateTime.Now.ToString("mm"));
            if (now == 49)
            {
                if (!backUpFlag)
                {
                    backUpFlag = !backUpFlag;
                    return true;
                }
            }
            else backUpFlag = false;
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
                var now = DateTime.Now.ToString("HH:mm");
                foreach (var rule in ruleListClone)
                {
                    // Check if rule's scenario is Active
                    if (!rule.Scenario.isActive) continue;

                    // Check if user set a END TIME trigger
                    if (rule.EndTime != null)
                    {
                        // Check if TIME is now                        
                        var endTime = rule.EndTime?.ToString("HH:mm");
                        if (now.Equals(endTime))
                        {
                            ruleTriggerManager.Notify(rule.DeviceId, "STATE", 0);
                            continue;
                        }
                    }

                    // Check if user set a TIME trigger
                    if (rule.StartTime != null)
                    {
                        // Check if TIME is now
                        var startTime = rule.StartTime?.ToString("HH:mm");
                        if (!now.Equals(startTime)) continue;
                    }

                    // Check if user set an API trigger
                    if (rule.APIKey != null)
                    {
                        // Check if API value is triggered
                        string value;
                        if (!apiData.TryGetValue(rule.APIKey, out value)) continue;
                        if (value != rule.ApiValue) continue;
                    }

                    // Check if user set an ACTION TRIGGER
                    if (rule.ActionTrigger != null)
                    {
                        Random random = new Random();
                        if (random.Next(2) == 0) return;
                    }


                    Console.WriteLine("Trigger Detected: " + rule.RuleName);
                    var deviceID = rule.DeviceId;
                    var configKey = rule.ConfigurationKey;
                    var configValue = rule.ConfigurationValue;

                    // Retrieve revised config values from EnergyProfile component
                    int adjustedConfigValue = await _energyProfileInterface.getRevisedConfigValue(rule.Device.AccountId, deviceID, configKey, configValue);

                    // Notify all observers of the device trigger
                    ruleTriggerManager.Notify(deviceID, configKey, adjustedConfigValue);
                    ruleTriggerManager.Notify(deviceID, "STATE", 1);

                    /*
                     *  Prepare to store device trigger in db by retrieving information
                     */
                    var configMeaning = string.Format("{0} is set to {1}", configKey, configValue); // await getConfigMeaning(deviceID, configKey, configValue);
                    // Check if rulehistory exists and matches rule
                    var storedRule = await _ruleHistoryRepository.GetByRuleIdAsync(rule.RuleId);
                    Guid ruleHistoryId;
                    if (storedRule == null)
                    {
                        Console.WriteLine("Nothing stored!");
                        ruleHistoryId = Guid.NewGuid();
                        RuleHistory rh = new RuleHistory
                        {
                            RuleHistoryId = ruleHistoryId,
                            RuleId = rule.RuleId,
                            RuleIndex = 0,
                            RuleName = rule.RuleName,
                            RuleStartTime = rule.StartTime,
                            RuleEndTime = rule.EndTime,
                            RuleActionTrigger = rule.ActionTrigger,
                            APIKey = rule.APIKey,
                            ApiValue = rule.ApiValue,
                            ScenarioName = rule.Scenario.ScenarioName,
                            DeviceName = rule.Device.DeviceName,
                            DeviceConfiguration = configMeaning
                        };
                        await _ruleHistoryRepository.AddAsync(rh);
                    }
                    else if (!checkIfRuleMatch(rule, storedRule))
                    {
                        Console.WriteLine("Stored not matching!");
                        ruleHistoryId = Guid.NewGuid();
                        RuleHistory rh = new RuleHistory
                        {
                            RuleHistoryId = ruleHistoryId,
                            RuleId = rule.RuleId,
                            RuleIndex = storedRule.RuleIndex + 1,
                            RuleName = rule.RuleName,
                            RuleStartTime = rule.StartTime,
                            RuleEndTime = rule.EndTime,
                            RuleActionTrigger = rule.ActionTrigger,
                            APIKey = rule.APIKey,
                            ApiValue = rule.ApiValue,
                            ScenarioName = rule.Scenario.ScenarioName,
                            DeviceName = rule.Device.DeviceName,
                            DeviceConfiguration = configMeaning
                        };
                        await _ruleHistoryRepository.AddAsync(rh);
                    }
                    else
                    {
                        ruleHistoryId = storedRule.RuleHistoryId;
                    }

                    // Log history
                    History h = new History
                    {
                        Message = configMeaning,
                        Timestamp = DateTime.Now,
                        DeviceAdjustedConfiguration = adjustedConfigValue,
                        ProfileId = rule.Scenario.ProfileId,
                        RuleHistoryId = ruleHistoryId,
                    };

                    await _historyRepository.AddAsync(h);
                }
            }
        }

        public bool checkIfRuleMatch(Rule rule, RuleHistory ruleHistory)
        {
            if (rule.RuleName == ruleHistory.RuleName
                && rule.StartTime == ruleHistory.RuleStartTime
                && rule.EndTime == ruleHistory.RuleEndTime
                && rule.ActionTrigger == ruleHistory.RuleActionTrigger
                && rule.APIKey == ruleHistory.APIKey
                && rule.ApiValue == ruleHistory.ApiValue
                && rule.Scenario.ScenarioName == ruleHistory.ScenarioName
                && rule.Device.DeviceName == ruleHistory.DeviceName
                && string.Format("{0} is set to {1}", rule.ConfigurationKey, rule.ConfigurationValue) == ruleHistory.DeviceConfiguration
                ) return true;
            return false;
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
                    DeviceConfiguration = string.Format("{0} is set to {1}", rule.ConfigurationKey, rule.ConfigurationValue) // await getConfigMeaning(rule.Device.DeviceId, rule.ConfigurationKey, rule.ConfigurationValue);
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

        public async void executeSecurityProtocol(Guid accountId, bool setTo, HomeSecurityDeviceDefinition homeSecurityDeviceDefinition)
        {
            bool successState = false;
            if (setTo)
            {
                successState = await _directorControlDeviceInterface.SetDeviceTypeConfiguration
                (accountId, homeSecurityDeviceDefinition.DeviceGroup, homeSecurityDeviceDefinition.ConfigurationKey, homeSecurityDeviceDefinition.ConfigurationOnValue);
            }
            else
            {
                successState = await _directorControlDeviceInterface.SetDeviceTypeConfiguration
                (accountId, homeSecurityDeviceDefinition.DeviceGroup, homeSecurityDeviceDefinition.ConfigurationKey, homeSecurityDeviceDefinition.ConfigurationOffValue);
            }

            if (successState)
            {
                Console.WriteLine(string.Format("Successfully executed security protocol for: {0} - {1} from {2} to {3}!",
                    accountId, homeSecurityDeviceDefinition.DeviceGroup,
                    setTo ? homeSecurityDeviceDefinition.ConfigurationOffValue : homeSecurityDeviceDefinition.ConfigurationOnValue,
                    setTo ? homeSecurityDeviceDefinition.ConfigurationOnValue : homeSecurityDeviceDefinition.ConfigurationOffValue));
            }
            else
            {
                Console.WriteLine(string.Format("Failed to execute security protocol for: {0} - {1} from {2} to {3}!",
                    accountId, homeSecurityDeviceDefinition.DeviceGroup,
                    setTo ? homeSecurityDeviceDefinition.ConfigurationOffValue : homeSecurityDeviceDefinition.ConfigurationOnValue,
                    setTo ? homeSecurityDeviceDefinition.ConfigurationOnValue : homeSecurityDeviceDefinition.ConfigurationOffValue));
            }
        }
    }
}
