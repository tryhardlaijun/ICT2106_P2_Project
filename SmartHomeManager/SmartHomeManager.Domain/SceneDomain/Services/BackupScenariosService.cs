using System;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class BackupScenariosService : IBackupScenariosService
	{
        private readonly IBackupScenariosRepository _backupScenariosRepository;
        private readonly IInformDirectorServices _informDirectorServices;

        public BackupScenariosService(IBackupScenariosRepository backupScenariosRepository, IInformDirectorServices informDirectorServices)
        {
            _backupScenariosRepository = backupScenariosRepository;
            _informDirectorServices = informDirectorServices;
        }

        public async Task<bool> LoadScenariosBackup(Guid profileId, IEnumerable<Scenario> scenarios)
        {
            try
            {
                foreach (var scenario in scenarios)
                {
                    await _backupScenariosRepository.DeleteScenario(scenario);
                    _informDirectorServices.InformScenarioChangesAsync(scenario.ScenarioId, 'd');
                    await _backupScenariosRepository.CreateScenario(scenario);
                    _informDirectorServices.InformScenarioChangesAsync(scenario.ScenarioId, 'c');
                }
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}

