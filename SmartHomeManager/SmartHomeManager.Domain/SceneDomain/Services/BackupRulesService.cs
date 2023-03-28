using System;
using System.Data;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using Rule = SmartHomeManager.Domain.SceneDomain.Entities.Rule;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class BackupRulesService: IBackupRulesService
    {
        private readonly IBackupRulesRepository _backupRulesRepository;
        private readonly IInformDirectorServices _informDirectorServices;
        
		public BackupRulesService(IBackupRulesRepository backupRulesRepository, IInformDirectorServices informDirectorServices)
		{
            _backupRulesRepository = backupRulesRepository;
            _informDirectorServices = informDirectorServices;
        }

        public async Task<bool> LoadRulesBackup(Guid profileId, List<Rule> rules)
        {
            Console.WriteLine(rules.Count.ToString());
            try
            {
                
                foreach (var rule in rules)
                {
                    Console.WriteLine(rule.RuleName);
                    await _backupRulesRepository.DeleteRule(rule);
                    await _informDirectorServices.InformRuleChangesAsync(rule.RuleId, 'd');
                    await _backupRulesRepository.CreateRule(rule);
                    await _informDirectorServices.InformRuleChangesAsync(rule.RuleId, 'c');
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }           
        }
    }
}

