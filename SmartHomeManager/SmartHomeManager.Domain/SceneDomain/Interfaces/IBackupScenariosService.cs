using System;
using SmartHomeManager.Domain.SceneDomain.Entities;
namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
	public interface IBackupScenariosService
	{
        Task<bool> LoadScenariosBackup(Guid profileId, IEnumerable<Scenario> rules);
    }
}

