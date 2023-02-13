using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
	public interface IGetRulesServices
	{
        // get all rules on startup
        Task<IEnumerable<Rule>> GetAllRules();

        // refresh all rules of a scenario when director is informed of a change
        Task<IEnumerable<Rule>> GetAllRulesByScenarioId(Guid ScenarioId); 
    }
}

