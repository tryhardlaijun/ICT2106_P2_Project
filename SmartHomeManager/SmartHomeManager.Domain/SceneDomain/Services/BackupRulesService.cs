using System;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class BackupRulesService: IBackupRulesService
    {
        private readonly IBackupRulesRepository _backupRulesRepository;
        
		public BackupRulesService(IBackupRulesRepository backupRulesRepository)
		{
            _backupRulesRepository = backupRulesRepository;
        }

        public async Task<bool> LoadRulesBackup(Guid profileId, IEnumerable<Rule> rules)
        {
            return await _backupRulesRepository.LoadRulesBackup(profileId, rules);
        }
    }
}

