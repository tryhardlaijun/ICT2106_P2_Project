using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.BackupDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.BackupDomain.Services
{
    public class BackupServices : IUpdateBackupService, IBackupService
    {
        private readonly IBackupRuleRepository _backupRuleRepository;
        private readonly IBackupScenarioRepository _backupScenarioRepository;
        public BackupServices(IBackupRuleRepository backupRuleRepository, IBackupScenarioRepository backupScenarioRepository)
        {
            _backupRuleRepository = backupRuleRepository;
            _backupScenarioRepository = backupScenarioRepository;
        }

        public async void createBackup(List<Rule> rulesList, List<Scenario> scenarioList)
        {
            foreach (var rule in rulesList)
            {
                BackupRule backupRule = new BackupRule
                {
                    rulesID = rule.RuleId,
                    scenarioID = rule.ScenarioId,
                    scheduleName = rule.RuleName,
                    startTime = (DateTime)rule.StartTime,
                    endTime = (DateTime)rule.EndTime,
                    actionTrigger = rule.ActionTrigger,
                    configurationKey = rule.ConfigurationKey,
                    configurationValue = rule.ConfigurationValue,
                    apiKey = rule.APIKey,
                    apiValue = rule.ApiValue
                };
                await _backupRuleRepository.CreateBackupRule(backupRule);
            }

            foreach (var scenario in scenarioList)
            {
                BackupScenario backupScenario = new BackupScenario
                {
                    scenarioID = scenario.ScenarioId,
                    scheduleName = scenario.ScenarioName,
                    profileID = scenario.ProfileId
                };
                await _backupScenarioRepository.CreateBackupScenario(backupScenario);
            }
        }

        public async Task<List<BackupRule>> loadBackupRule(Guid scenarioId)
        {
            return await _backupRuleRepository.GetBackupRuleById(scenarioId);
        }
        public async Task<List<BackupScenario>> loadBackupScenario(Guid profileId)
        {
            return await _backupScenarioRepository.GetBackupScenarioById(profileId);
        }

        public async Task<IEnumerable<BackupScenario>> getAllBackupScenario()
        {
            return await _backupScenarioRepository.GetAllBackupScenario();
        }

        public async Task<IEnumerable<BackupRule>> getAllBackupRule()
        {
            return await _backupRuleRepository.GetAllBackupRule();
        }

        public async void restoreBackupComplete()
        {
            throw new NotImplementedException();
            //return wait unpauseTrigger
        }
    }
}
