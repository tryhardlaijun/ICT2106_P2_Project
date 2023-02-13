using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.Common;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class GetRulesServices: IGetRulesServices
	{
        private readonly IGenericRepository<Rule> _ruleRepository;
        public GetRulesServices(IGenericRepository<Rule> ruleRepository)
		{
            _ruleRepository = ruleRepository;
		}
        
        public Task<IEnumerable<Rule>> GetAllRules()
        {
            return _ruleRepository.GetAllAsync();
        }

        public Task<IEnumerable<Rule>> GetAllRulesByScenarioId(Guid ScenarioId)
        {
            throw new NotImplementedException();
        }
    }
}

