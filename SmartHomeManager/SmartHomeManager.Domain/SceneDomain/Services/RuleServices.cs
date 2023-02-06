using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;


namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class RuleServices
	{
		private readonly IGenericRepository<Rule> _ruleRepository;

		public RuleServices(IGenericRepository<Rule> ruleRepository)
		{
			_ruleRepository = ruleRepository;
        }

		public async Task<IEnumerable<Rule>> GetAllRulesAsync()
		{
			return await _ruleRepository.GetAllAsync();
		}

		//public async Task<Rule?> GetRuleByIdAsync(Guid id)
		//{
		//	return await _ruleRepository.GetByIdAsync(id);
		//}

		public async Task<bool> CreateRuleAsync(Rule rule)
		{
			return await _ruleRepository.AddAsync(rule);
		}

		//public async Task<bool> EditRuleAsync(Rule rule)
		//{
		//	return await _ruleRepository.UpdateAsync(rule);
		//}

		//public async Task<bool> DeleteRuleByIdAsync(Guid id)
		//{
		//	return await _ruleRepository.DeleteByIdAsync(id);
		//}
	}
}

