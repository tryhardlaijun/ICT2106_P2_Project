using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
	public interface IBackupRulesService
	{
        Task<bool> LoadRulesBackup(Guid profileId, IEnumerable<Rule> rules);
    }
}

