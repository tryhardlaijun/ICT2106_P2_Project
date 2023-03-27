using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
    public interface IBackupRulesRepository
    {


        Task<bool> DeleteRule(Domain.SceneDomain.Entities.Rule rule);
        Task<bool> CreateRule(Domain.SceneDomain.Entities.Rule rule);

    }
}

