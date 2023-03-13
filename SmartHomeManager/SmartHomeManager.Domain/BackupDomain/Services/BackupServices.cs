using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.BackupDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.BackupDomain.Services
{
    public class BackupServices : IUpdateBackupService, IBackupService
    {
        private readonly IBackupRuleRepository _backupRuleRepository;
        private readonly IBackupScenarioRepository _backupScenarioRepository;

        private readonly IBackupRulesService _backupRuleInterface;
        private readonly IBackupScenariosService _backupScenarioInterface;

        private List<BackupRule> backupRulesList = new List<BackupRule>();
        private List<BackupScenario> backupScenarioList = new List<BackupScenario>();

        private List<Rule> rulesList = new List<Rule>();
        private List<Scenario> scenarioList = new List<Scenario>();

        public BackupServices(IBackupRuleRepository backupRuleRepository, IBackupScenarioRepository backupScenarioRepository, IBackupRulesService backupRulesService)
        {
            _backupRuleRepository = backupRuleRepository;
            _backupScenarioRepository = backupScenarioRepository;

            _backupRuleInterface = backupRulesService;
        }


        public async void createBackup(List<Rule> rulesList, List<Scenario> scenarioList)
        {
            var now = DateTime.Now;
            Guid backupId = Guid.NewGuid();
            
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
                BackupRule backupRule = new BackupRule
                {
                    RuleId = rule.RuleId,
                    ScenarioId = rule.ScenarioId,
                    RuleName = rule.RuleName,
                    StartTime = (DateTime)rule.StartTime,
                    EndTime = (DateTime)rule.EndTime,
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

        public async Task<List<BackupRule>> loadBackupRule(Guid backupId)
        {
            var tempRulesList = new List<Rule>();
            backupRulesList = await _backupRuleRepository.GetBackupRuleById(backupId);

            foreach (var backupRule in backupRulesList)
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

                tempRulesList.Add(rule);
            }
            rulesList = tempRulesList;

            //call interface methods here to restore backup
            
            return backupRulesList;
        }
        public async Task<List<BackupScenario>> loadBackupScenario(Guid profileId)
        {
            var tempScenarioList = new List<Scenario>();
            backupScenarioList = await _backupScenarioRepository.GetBackupScenarioById(profileId);
            foreach (var backupScenario in backupScenarioList)
            {
                Scenario scenario = new Scenario { 
                    isActive = false,
                    ScenarioName= backupScenario.ScenarioName,
                    ProfileId = profileId,
                    ScenarioId = backupScenario.ScenarioId
                };

                tempScenarioList.Add(scenario);
            }

            scenarioList = tempScenarioList;

            //call interface methods here to restore backup

            return backupScenarioList;
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
