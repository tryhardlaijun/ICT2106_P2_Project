using System;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class BackupScenariosService : IBackupScenariosService
	{
        private readonly IBackupScenariosRepository _backupScenariosRepository;

        public BackupScenariosService(IBackupScenariosRepository backupScenariosRepository)
        {
            _backupScenariosRepository = backupScenariosRepository;
        }

        public async Task<bool> LoadScenariosBackup(Guid profileId, IEnumerable<Scenario> scenarios)
        {
            try
            {
                return await _backupScenariosRepository.LoadScenariosBackup(profileId, scenarios);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}

