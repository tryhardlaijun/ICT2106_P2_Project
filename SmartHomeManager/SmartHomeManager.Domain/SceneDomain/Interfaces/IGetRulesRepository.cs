using System;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
    // interface for repo
	public interface IGetRulesRepository
	{
        Task<IEnumerable<Rule>> GetAllRulesByScenarioIdAsync(Guid ScenarioId);
    }
}

