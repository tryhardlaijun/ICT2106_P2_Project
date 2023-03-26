using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
	public interface IScenarioRepository
    {
        // get all rules on startup and when director is informed
        Task<IEnumerable<Scenario>> GetAllScenarios();

        Task<Scenario> GetScenarioById(Guid id);
    }
}

