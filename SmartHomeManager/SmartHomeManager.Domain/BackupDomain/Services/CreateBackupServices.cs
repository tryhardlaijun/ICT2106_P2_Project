using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.BackupDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.BackupDomain.Services
{
    public class CreateBackupServices : ICreateBackupService
    {
        private readonly IBackupRuleRepository _backupRuleRepository;
        private readonly IBackupScenarioRepository _backupScenarioRepository;        

        public CreateBackupServices(IBackupRuleRepository backupRuleRepository, IBackupScenarioRepository backupScenarioRepository)
        {
            _backupRuleRepository = backupRuleRepository;
            _backupScenarioRepository = backupScenarioRepository;            
        }

        //used in director
        public async void createBackup(List<Rule> rulesList, List<Scenario> scenarioList)
        {
            var now = DateTime.UtcNow;
            Guid backupId = Guid.NewGuid();
            Console.WriteLine("Backing up rules and scenarios...");

            foreach (var scenario in scenarioList)
            {
                BackupScenario backupScenario = new BackupScenario
                {
                    ScenarioId = scenario.ScenarioId,
                    ScenarioName = scenario.ScenarioName,
                    ProfileId = scenario.ProfileId,
                    CreatedAt = now,
                    BackupId = backupId
                };
                await _backupScenarioRepository.CreateBackupScenario(backupScenario);
            }

            foreach (var rule in rulesList)
            {
                Console.WriteLine(rule.RuleName);
                BackupRule backupRule = new BackupRule
                {
                    RuleId = rule.RuleId,
                    ScenarioId = rule.ScenarioId,
                    RuleName = rule.RuleName,
                    StartTime = rule.StartTime,
                    EndTime = rule.EndTime,
                    ActionTrigger = rule.ActionTrigger,
                    ConfigurationKey = rule.ConfigurationKey,
                    ConfigurationValue = rule.ConfigurationValue,
                    APIKey = rule.APIKey,
                    ApiValue = rule.ApiValue,
                    DeviceId = rule.DeviceId,
                    BackupId = backupId
                };
                await _backupRuleRepository.CreateBackupRule(backupRule);
            }
        }

        

    }
}
