using System;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

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

        public async Task<bool> LoadRulesBackup(Guid profileId, IEnumerable<Rule> rules)
        {
            try
            {
                foreach (var rule in rules)
                {
                    await _backupRulesRepository.DeleteRule(rule);
                    _informDirectorServices.InformRuleChangesAsync(rule.RuleId, 'd');
                    await _backupRulesRepository.CreateRule(rule);
                    _informDirectorServices.InformRuleChangesAsync(rule.RuleId, 'c');
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

