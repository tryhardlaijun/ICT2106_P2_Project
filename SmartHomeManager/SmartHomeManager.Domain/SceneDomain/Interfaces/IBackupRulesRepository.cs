using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
    public interface IBackupRulesRepository
    {
        Task<bool> LoadRulesBackup(Guid profileId, IEnumerable<Rule> rules);
    }
}

