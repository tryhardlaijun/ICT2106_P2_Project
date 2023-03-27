using System;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
    // interface for repo
	public interface IGetRulesRepository
	{
        Task<IEnumerable<Rule>> GetAllRules();
        Task<Rule?> GetAllRulesById(Guid RuleID);
        Task<IEnumerable<Rule>> GetAllRulesByScenarioIdAsync(Guid ScenarioId);

        Task<IEnumerable<Rule>> GetSchedulesByScenarioIdAsync(Guid ScenarioId);
        Task<IEnumerable<Rule>> GetEventsByScenarioIdAsync(Guid ScenarioId);
        Task<IEnumerable<Rule>> GetApiByScenarioIdAsync(Guid ScenarioId);
    }
}

