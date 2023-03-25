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
    public class RuleServices: IGetRulesService
    {
        private readonly IGenericRepository<Rule> _ruleRepository;
        private readonly IGetRulesRepository _getRuleRepository;
        private readonly IInformDirectorServices _informDirectorServices;

        //Initialise the service by passing the repo
        public RuleServices(IGenericRepository<Rule> ruleRepository,IGetRulesRepository getRulesRepository, IInformDirectorServices informDirectorServices)
        {
            _ruleRepository = ruleRepository;
            _informDirectorServices = informDirectorServices;
            _getRuleRepository = getRulesRepository;
        }

        #region CRUD Region
        //Create Rule
        public async Task<bool> CreateRuleAsync(Rule rule)
        {
            if (await _ruleRepository.AddAsync(rule))
            {
                _informDirectorServices.InformRuleChangesAsync(rule.RuleId, 'c');
                return true;
            }
            return false;
        }

        //Update Rule
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

        #region Provided Interface
        // Get all rules associated with scenario
        public async Task<IEnumerable<Rule?>> GetAllRulesByScenarioIdAsync(Guid ScenarioId)
        {
            return await _getRuleRepository.GetAllRulesByScenarioIdAsync(ScenarioId);
        }

        public async Task<IEnumerable<Rule?>> GetEventsByScenarioIdAsync(Guid ScenarioId)
        {
            return await _getRuleRepository.GetEventsByScenarioIdAsync(ScenarioId);
        }

        public async Task<IEnumerable<Rule?>> GetApisByScenarioIdAsync(Guid ScenarioId)
        {
            return await _getRuleRepository.GetApiByScenarioIdAsync(ScenarioId);
        }

        public async Task<IEnumerable<Rule?>> GetSchedulesByScenarioIdAsync(Guid ScenarioId)
        {
            return await _getRuleRepository.GetSchedulesByScenarioIdAsync(ScenarioId);
        }


        //Get using id
        public async Task<Rule?> GetRuleByIdAsync(Guid id)
        {
            return await _ruleRepository.GetByIdAsync(id);
        }

        //Get all the rules 
        public async Task<IEnumerable<Rule>> GetAllRulesAsync()
        {
            return await _ruleRepository.GetAllAsync();
        }
        #endregion

        #endregion

        // Upload the json file
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

        public async Task<byte[]> DownloadRules(Guid ScenarioId)
        {
            var allRules = await GetAllRulesByScenarioIdAsync(ScenarioId);
            var ruleJson = JsonConvert.SerializeObject(allRules.ToList(), Formatting.Indented);
            return Encoding.UTF8.GetBytes(ruleJson);
        }

        // Detect clashes
        public async Task<bool> RuleClashesAsync(RuleRequest rule)
        {
            var existingRules = await GetAllRulesAsync();
            if(rule.StartTime != null)
            {
                foreach (var existingRule in existingRules)
                {
                    if (existingRule.StartTime == null || existingRule.EndTime == null)
                    {
                        continue;
                    }

                    bool sameScenario = existingRule.ScenarioId == rule.ScenarioId;
                    bool sameDevice = existingRule.DeviceId == rule.DeviceId;
                    bool sameConfigurationKey = existingRule.ConfigurationKey == rule.ConfigurationKey;
                    bool timeOverlap = existingRule.StartTime?.TimeOfDay < rule.EndTime?.TimeOfDay && existingRule.EndTime?.TimeOfDay > rule.StartTime?.TimeOfDay;
                    bool differentRule = existingRule.RuleId != rule.RuleId;
                    if (sameScenario && sameDevice && sameConfigurationKey && timeOverlap && differentRule)
                    {
                        return true; // clash found
                    }
                }
            }
            return false; // no clash found
        }
    }
}
