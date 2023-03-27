using System;
using SmartHomeManager.Domain.SceneDomain.Entities;
namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
	public interface IBackupScenariosRepository
	{
        Task<bool> DeleteScenario(Scenario scenario);

        Task<bool> CreateScenario(Scenario scenario);
    }
}

