using System;
using SmartHomeManager.Domain.SceneDomain.Entities;
namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
	public interface IBackupScenarioRepository
	{
        Task<bool> LoadScenariosBackup(Guid profileId, IEnumerable<Scenario> rules);
    }
}

