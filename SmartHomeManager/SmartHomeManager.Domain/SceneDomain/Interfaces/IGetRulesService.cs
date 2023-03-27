using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
    //Provided service
    public interface IGetRulesService
	{
        //// get all rules on startup
        Task<IEnumerable<RuleRequest>> GetAllRulesAsync();

        Task<RuleRequest> GetRuleByIdAsync(Guid id);

        // refresh all rules of a scenario when director is informed of a change
        Task<IEnumerable<RuleRequest?>> GetAllRulesByScenarioIdAsync(Guid ScenarioId); 
    }
}

