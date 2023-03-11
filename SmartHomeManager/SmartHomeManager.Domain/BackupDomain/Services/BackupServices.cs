using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.BackupDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
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

        private readonly IBackupRulesService _backupRuleInterface;
        private readonly IBackupScenarioService _backupScenarioInterface;

        private List<Rule> backupRulesList;
        private List<Scenario> backupScenarioList;

        public BackupServices(IBackupRuleRepository backupRuleRepository, IBackupScenarioRepository backupScenarioRepository)
        {
            _backupRuleRepository = backupRuleRepository;
            _backupScenarioRepository = backupScenarioRepository;
        }

        public async void createBackup(List<Rule> rulesList, List<Scenario> scenarioList)
        {
            var now = DateTime.Now;
            float highestVersionNo = 1.0f;

            foreach (var s in _backupScenarioRepository.GetAllBackupScenario().Result)
            {
                if (s.versionNumber >= highestVersionNo)
                {
                    highestVersionNo = (float)s.versionNumber;
                }
            }
            
            foreach (var scenario in scenarioList)
            {
                BackupScenario backupScenario = new BackupScenario
                {
                    scenarioID = scenario.ScenarioId,
                    scheduleName = scenario.ScenarioName,
                    profileID = scenario.ProfileId,
                    createdAt = now,
                    versionNumber = highestVersionNo + 0.1f
                };
                await _backupScenarioRepository.CreateBackupScenario(backupScenario);
            }

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
                    apiValue = rule.ApiValue,
                    versionNumber = highestVersionNo + 0.1f
                };
                await _backupRuleRepository.CreateBackupRule(backupRule);
            }
        }

        public async Task<List<BackupRule>> loadBackupRule(Guid scenarioId) //public async Task<List<Rule>> loadBackupRule(Guid scenarioId)
        {
            return await _backupRuleRepository.GetBackupRuleById(scenarioId); //return await _backupRuleInterface.loadRulesBackup(scenarioId, backupRulesList);
        }
        public async Task<List<BackupScenario>> loadBackupScenario(Guid profileId) //public async Task<List<Scenario>> loadBackupScenario(Guid profileId)
        {
            return await _backupScenarioRepository.GetBackupScenarioById(profileId); //return await _backupScenarioInterface.loadScenarioBackup(profileId, backupScenarioList);
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
