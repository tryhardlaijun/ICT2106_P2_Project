using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;


namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class RuleServices
	{
		private readonly IGenericRepository<Rule> _ruleRepository;

		//Initialise the service by passing the repo
		public RuleServices(IGenericRepository<Rule> ruleRepository)
		{
			_ruleRepository = ruleRepository;
        }

		//Get all
		public async Task<IEnumerable<Rule>> GetAllRulesAsync()
		{
			return await _ruleRepository.GetAllAsync();
		}

		//Get using id
		public async Task<Rule?> GetRuleByIdAsync(Guid id)
		{
			return await _ruleRepository.GetByIdAsync(id);
		}

		//Create
		public async Task<bool> CreateRuleAsync(RuleRequest ruleRequest)
		{
				var rule = new Rule
				{
					RuleId = ruleRequest.RuleId,
					ScenarioId = ruleRequest.ScenarioId,
                    ConfigurationValue = ruleRequest.ConfigurationValue,
					ActionTrigger = ruleRequest.ActionTrigger,
					ScheduleName = ruleRequest.ScheduleName,
					StartTime = Convert.ToDateTime(ruleRequest.StartTime),
					EndTime = Convert.ToDateTime(ruleRequest.EndTime),
					DeviceId = ruleRequest.DeviceId
				};
            return await _ruleRepository.AddAsync(rule);
		}

		//Update
		public async Task<bool> EditRuleAsync(Rule rule)
		{
			return await _ruleRepository.UpdateAsync(rule);
		}

		//Delete using Id
		public async Task<bool> DeleteRuleByIdAsync(Guid id)
		{
			return await _ruleRepository.DeleteByIdAsync(id);
		}
	}
}

