using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.BackupDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.BackupDomain.Services
{
    public class BackupServices
    {
        private readonly IBackupRuleRepository _backupRuleRepository;
        private readonly IBackupScenarioRepository _backupScenarioRepository;

        private readonly IBackupRulesService _backupRuleInterface;
        private readonly IBackupScenariosService _backupScenarioInterface;
        public BackupServices(IBackupRuleRepository backupRuleRepository, IBackupScenarioRepository backupScenarioRepository, IBackupRulesService backupRulesService, IBackupScenariosService backupsScenariosService)
        {
            _backupRuleRepository = backupRuleRepository;
            _backupScenarioRepository = backupScenarioRepository;

            _backupRuleInterface = backupRulesService;
            _backupScenarioInterface = backupsScenariosService;
        }

        //used in controller restoreBackup
        public async Task<List<BackupRule>> loadBackupRule(Guid profileId, Guid backupId, List<Guid> scenarioIdList)
        {
            var rulesList = new List<Rule>();
            var backupRulesList = await _backupRuleRepository.GetAllBackupRuleByBackupId(backupId);


            foreach (var backupRule in backupRulesList)
            {
                if (scenarioIdList.Any() && scenarioIdList.Contains(backupRule.ScenarioId))
                {
                    Rule rule = new Rule
                    {
                        RuleId = backupRule.RuleId,
                        ScenarioId = backupRule.ScenarioId,
                        RuleName = backupRule.RuleName,
                        StartTime = backupRule.StartTime,
                        EndTime = backupRule.EndTime,
                        ActionTrigger = backupRule.ActionTrigger,
                        ConfigurationKey = backupRule.ConfigurationKey,
                        ConfigurationValue = backupRule.ConfigurationValue,
                        APIKey = backupRule.APIKey,
                        ApiValue = backupRule.ApiValue,
                        DeviceId = backupRule.DeviceId
                    };

                    rulesList.Add(rule);
                }

            }
            Console.WriteLine(rulesList.Count().ToString());
            await _backupRuleInterface.LoadRulesBackup(profileId, rulesList);
            return backupRulesList;
        }

        //used in controller restoreBackup
        public async Task<List<BackupScenario>> loadBackupScenario(Guid profileId, List<Guid> scenarioIdList)
        {
            var scenarioList = new List<Scenario>();
            var backupScenarioList = await _backupScenarioRepository.GetAllBackupScenarioByProfileId(profileId);
            foreach (var backupScenario in backupScenarioList)
            {
                if (scenarioIdList.Any() && scenarioIdList.Contains(backupScenario.ScenarioId))
                {
                    Scenario scenario = new Scenario
                    {
                        isActive = false,
                        ScenarioName = backupScenario.ScenarioName,
                        ProfileId = profileId,
                        ScenarioId = backupScenario.ScenarioId
                    };

                    scenarioList.Add(scenario);
                }
            }

            await _backupScenarioInterface.LoadScenariosBackup(profileId, scenarioList);
            return backupScenarioList;
        }

        //used in controller getAllBackupScenario
        public async Task<IEnumerable<BackupScenario>> getAllBackupScenarioByProfileId(Guid profileId)
        {
            return await _backupScenarioRepository.GetAllBackupScenarioByProfileId(profileId);
        }
    }
        
}
