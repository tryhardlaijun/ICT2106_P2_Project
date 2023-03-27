using System;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class GetRulesService : IGetRulesService
	{
        private readonly IGetRulesRepository _getRuleRepository;
        public GetRulesService(IGetRulesRepository getRulesRepository)
		{
            _getRuleRepository = getRulesRepository;
        }

        public async Task<IEnumerable<Rule>> GetAllRulesAsync()
        {
            return await _getRuleRepository.GetAllRules();
        }

        public async Task<IEnumerable<Rule?>> GetAllRulesByScenarioIdAsync(Guid ScenarioId)
        {
            return await _getRuleRepository.GetAllRulesByScenarioIdAsync(ScenarioId);
        }

        public async Task<Rule?> GetRuleByIdAsync(Guid id)
        {
            return await _getRuleRepository.GetAllRulesById(id);
        }
    }
}

