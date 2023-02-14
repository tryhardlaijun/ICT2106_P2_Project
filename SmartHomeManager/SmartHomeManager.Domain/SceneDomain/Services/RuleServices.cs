using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.DirectorDomain.Services;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System.Data;
using Rule = SmartHomeManager.Domain.SceneDomain.Entities.Rule;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class RuleServices
	{
		private readonly IGenericRepository<Rule> _ruleRepository;
		private readonly IInformDirectorServices _informDirectorServices;

		//Initialise the service by passing the repo
		public RuleServices(IGenericRepository<Rule> ruleRepository, IInformDirectorServices informDirectorServices)
		{
			_ruleRepository = ruleRepository;
			_informDirectorServices = informDirectorServices;
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
		public async Task<bool> CreateRuleAsync(Rule rule)
		{
			_informDirectorServices.InformRuleChangesAsync(rule.RuleId, 'c');
            return await _ruleRepository.AddAsync(rule);
		}

		//Update
		public async Task<bool> EditRuleAsync(Rule rule)
		{
            _informDirectorServices.InformRuleChangesAsync(rule.RuleId, 'e');
            return await _ruleRepository.UpdateAsync(rule);
		}

		//Delete using Id
		public async Task<bool> DeleteRuleByIdAsync(Guid id)
		{
            _informDirectorServices.InformRuleChangesAsync(id, 'd');
            return await _ruleRepository.DeleteByIdAsync(id);
		}
	}
}

