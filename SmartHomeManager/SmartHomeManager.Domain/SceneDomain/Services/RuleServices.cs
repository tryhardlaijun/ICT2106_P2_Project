using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.DirectorDomain.Services;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using System.Data;
using System.Text;
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
            if (await _ruleRepository.AddAsync(rule))
            {
                _informDirectorServices.InformRuleChangesAsync(rule.RuleId, 'c');
                return true;
            }

            return false;
        }

        //Update
        public async Task<bool> EditRuleAsync(Rule rule)
        {
            if (await _ruleRepository.UpdateAsync(rule))
            {
                _informDirectorServices.InformRuleChangesAsync(rule.RuleId, 'u');
                return true;
            }

            return false;
        }

        //Delete using Id
        public async Task<bool> DeleteRuleByIdAsync(Guid id)
        {
            if (await _ruleRepository.DeleteByIdAsync(id))
            {
                _informDirectorServices.InformRuleChangesAsync(id, 'd');
                return true;

            }

            return false;
        }

        public async Task<bool> UploadRules(IFormFile file)
        {
            var allCurrentRules = await GetAllRulesAsync();
            try
            {
                using (var stream = new StreamReader(file.OpenReadStream()))
                {
                    string fileContents = await stream.ReadToEndAsync();
                    List<Rule>? newRule = JsonConvert.DeserializeObject<List<Rule>>(fileContents);
                    if (newRule.Any() && newRule != null)
                    {
                        foreach (var rule in newRule)
                        {
                            var ruleCheck = await GetRuleByIdAsync(rule.RuleId);
                            if (ruleCheck != null)
                            {
                                Console.WriteLine(rule.RuleName + "already in DB");
                            }
                            else
                            {
                                await CreateRuleAsync(rule);
                                Console.WriteLine("Uploaded " + rule.RuleName);
                            }
                        }
                    }
                }
				return true;
            }
            catch (Exception ex)
            {
				Console.WriteLine(ex);
				return false;
            }
        }
        public async Task<bool> RuleClashesAsync(RuleRequest rule)
        {
            var existingRules = await GetAllRulesAsync();
            foreach (var existingRule in existingRules)
            {
                if (existingRule == null) continue;
                if (existingRule.ScenarioId == rule.ScenarioId && existingRule.ConfigurationKey == rule.ConfigurationKey && existingRule.ScenarioId == rule.ScenarioId && existingRule.DeviceId == rule.DeviceId
                    && existingRule.StartTime < rule.EndTime && existingRule.EndTime > rule.StartTime && existingRule.RuleId != rule.RuleId)
                {
                    return true; // clash found
                }
            }
            return false; // no clash found
        }
    }

}

