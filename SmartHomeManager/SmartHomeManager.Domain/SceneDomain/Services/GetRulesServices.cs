using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.DirectorDomain.Services;
using SmartHomeManager.Domain.Common;
using Newtonsoft.Json;
using System.Text;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
    public class GetRulesServices : IGetRulesService
    {
        private readonly IGetRulesRepository _getRuleRepository;
        public GetRulesServices(IGetRulesRepository ruleRepository)
        {
            _getRuleRepository = ruleRepository;
        }

        public async Task<IEnumerable<Rule>> GetAllRulesAsync()
        {
            return await _getRuleRepository.GetAllRulesAsync();
        }

        public async Task<IEnumerable<Rule?>> GetAllRulesByScenarioIdAsync(Guid ScenarioId)
        {
            return await _getRuleRepository.GetAllRulesByScenarioIdAsync(ScenarioId);
        }

        public async Task<Rule?> GetRuleByIdAsync(Guid id)
        {
            return await _getRuleRepository.GetRuleByIdAsync(id);
        }
        public async Task<byte[]> DownloadRules(Guid ScenarioId)
        {
            var allRules = await GetAllRulesByScenarioIdAsync(ScenarioId);
            var ruleJson = JsonConvert.SerializeObject(allRules.ToList(), Formatting.Indented);
            return Encoding.UTF8.GetBytes(ruleJson);
        }
    }
}

