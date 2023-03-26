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
        public async Task<RuleRequest?> RuleClashesAsync(RuleRequest ruleReq)
        {
            var existingRules = await GetAllRulesAsync();
            if (ruleReq.StartTime != null)
            {
                foreach (var existingRule in existingRules)
                {
                    if (existingRule.StartTime == null || existingRule.EndTime == null)
                    {
                        continue;
                    }

                    bool sameScenario = existingRule.ScenarioId == ruleReq.ScenarioId;
                    bool sameDevice = existingRule.DeviceId == ruleReq.DeviceId;
                    bool sameConfigurationKey = existingRule.ConfigurationKey == ruleReq.ConfigurationKey;
                    bool differentRule = existingRule.RuleId != ruleReq.RuleId;

                    bool timeOverlap = (
    // Standard overlap
    (existingRule.StartTime?.TimeOfDay < ruleReq.EndTime?.TimeOfDay && existingRule.EndTime?.TimeOfDay > ruleReq.StartTime?.TimeOfDay) ||

    // Existing rule starts within requested rule
    (existingRule.StartTime?.TimeOfDay >= ruleReq.StartTime?.TimeOfDay && existingRule.StartTime?.TimeOfDay < ruleReq.EndTime?.TimeOfDay) ||

    // Existing rule ends within requested rule
    (existingRule.EndTime?.TimeOfDay > ruleReq.StartTime?.TimeOfDay && existingRule.EndTime?.TimeOfDay <= ruleReq.EndTime?.TimeOfDay) ||

    // Both rules cross midnight
    ((ruleReq.StartTime?.TimeOfDay >= ruleReq.EndTime?.TimeOfDay && existingRule.StartTime?.TimeOfDay >= existingRule.EndTime?.TimeOfDay) &&
        (existingRule.StartTime?.TimeOfDay < ruleReq.EndTime?.TimeOfDay || existingRule.EndTime?.TimeOfDay > ruleReq.StartTime?.TimeOfDay)) ||

    // Only the existing rule crosses midnight
    (existingRule.StartTime?.TimeOfDay >= existingRule.EndTime?.TimeOfDay &&
        (existingRule.StartTime?.TimeOfDay < ruleReq.EndTime?.TimeOfDay || existingRule.EndTime?.TimeOfDay > ruleReq.StartTime?.TimeOfDay && existingRule.EndTime?.TimeOfDay <= ruleReq.EndTime?.TimeOfDay)) ||

    // Only the requested rule crosses midnight
    (ruleReq.StartTime?.TimeOfDay >= ruleReq.EndTime?.TimeOfDay &&
        (ruleReq.StartTime?.TimeOfDay < existingRule.EndTime?.TimeOfDay || ruleReq.EndTime?.TimeOfDay > existingRule.StartTime?.TimeOfDay && ruleReq.EndTime?.TimeOfDay <= existingRule.EndTime?.TimeOfDay)) ||

    // New rule completely overlaps the old rule, and both rules cross midnight
    (ruleReq.StartTime?.TimeOfDay <= existingRule.StartTime?.TimeOfDay && ruleReq.EndTime?.TimeOfDay >= existingRule.EndTime?.TimeOfDay &&
        ruleReq.StartTime?.TimeOfDay >= ruleReq.EndTime?.TimeOfDay && existingRule.StartTime?.TimeOfDay >= existingRule.EndTime?.TimeOfDay)
);




                    if (sameScenario && sameDevice && sameConfigurationKey && timeOverlap && differentRule)
                    {
                        var newReq = new RuleRequest
                        {
                            RuleId = existingRule.RuleId,
                            ScenarioId = existingRule.ScenarioId,
                            ConfigurationKey = existingRule.ConfigurationKey,
                            ConfigurationValue = existingRule.ConfigurationValue,
                            ActionTrigger = existingRule.ActionTrigger,
                            RuleName = existingRule.RuleName,
                            StartTime = (existingRule.StartTime != null) ? Convert.ToDateTime(existingRule.StartTime) : null,
                            EndTime = (existingRule.EndTime != null) ? Convert.ToDateTime(existingRule.EndTime) : null,
                            DeviceId = existingRule.DeviceId,
                            APIKey = existingRule.APIKey,
                            ApiValue = existingRule.ApiValue,
                        };
                        return newReq; // clash found
                    }
                }
            }
            return null; // no clash found
        }

    }
}


//bool timeOverlap = (
//    // This checks for a standard overlap, e.g., existing rule from 12:00 to 12:30 (12:00 PM to 12:30 PM) overlaps with requested rule from 11:00 to 13:00 (11:00 AM to 1:00 PM)
//    (existingRule.StartTime < ruleReq.EndTime && existingRule.EndTime > ruleReq.StartTime) ||

//    // This checks if the existing rule starts within the requested rule, e.g., existing rule from 12:00 to 13:30 (12:00 PM to 1:30 PM) overlaps with requested rule from 11:00 to 13:00 (11:00 AM to 1:00 PM)
//    (existingRule.StartTime >= ruleReq.StartTime && existingRule.StartTime < ruleReq.EndTime) ||

//    // This checks if the existing rule ends within the requested rule, e.g., existing rule from 10:30 to 12:30 (10:30 AM to 12:30 PM) overlaps with requested rule from 11:00 to 13:00 (11:00 AM to 1:00 PM)
//    (existingRule.EndTime > ruleReq.StartTime && existingRule.EndTime <= ruleReq.EndTime) ||

//    // To account for the overlap across midnight when both rules cross midnight
//    // Example: existing rule from 23:00 to 2:00 (11:00 PM to 2:00 AM) overlaps with requested rule from 1:00 to 4:00 (1:00 AM to 4:00 AM)
//    ((ruleReq.StartTime > ruleReq.EndTime && existingRule.StartTime > existingRule.EndTime) &&
//        ((existingRule.StartTime < ruleReq.EndTime) || (existingRule.EndTime > ruleReq.StartTime))) ||

//    // To account for the overlap when only the existing rule crosses midnight
//    // Example: existing rule from 23:00 to 1:00 (11:00 PM to 1:00 AM) overlaps with requested rule from 0:30 to 2:30 (12:30 AM to 2:30 AM)
//    (existingRule.StartTime > existingRule.EndTime &&
//        ((existingRule.StartTime < ruleReq.EndTime) || (existingRule.EndTime > ruleReq.StartTime && existingRule.EndTime <= ruleReq.EndTime))) ||

//    // To account for the overlap when only the requested rule crosses midnight
//    // Example: existing rule from 0:30 to 2:30 (12:30 AM to 2:30 AM) overlaps with requested rule from 23:00 to 1:00 (11:00 PM to 1:00 AM)
//    (ruleReq.StartTime > ruleReq.EndTime &&
//        ((ruleReq.StartTime < existingRule.EndTime) || (ruleReq.EndTime > existingRule.StartTime && ruleReq.EndTime <= existingRule.EndTime)))
//);
