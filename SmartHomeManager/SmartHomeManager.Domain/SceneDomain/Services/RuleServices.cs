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
        private readonly IRuleAdapter _ruleAdapter;

        //Initialise the service by passing the repo
        public RuleServices(IGenericRepository<Rule> ruleRepository,IGetRulesRepository getRulesRepository, IInformDirectorServices informDirectorServices)
        {
            _ruleRepository = ruleRepository;
            _informDirectorServices = informDirectorServices;
            _getRuleRepository = getRulesRepository;
            _ruleAdapter = new RuleAdapter();
        }

        #region CRUD Region
        //Create Rule
        public async Task<bool> CreateRuleAsync(RuleRequest ruleRequest)
        {
            var newRule = _ruleAdapter.ToRule(ruleRequest);
            if (await _ruleRepository.AddAsync(newRule))
            {
                _informDirectorServices.InformRuleChangesAsync(newRule.RuleId, 'c');
                return true;
            }
            return false;
        }

        //Update Rule
        public async Task<bool> EditRuleAsync(RuleRequest ruleRequest)
        {
            var rule = _ruleAdapter.ToRule(ruleRequest);
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
        public async Task<IEnumerable<RuleRequest?>> GetAllRulesByScenarioIdAsync(Guid ScenarioId)
        {
            var rules = await _getRuleRepository.GetAllRulesByScenarioIdAsync(ScenarioId);
            var resp = rules.Select(rule=> _ruleAdapter.ToRuleRequest(rule)).ToList();
            return resp;
        }

        public async Task<IEnumerable<RuleRequest?>> GetEventsByScenarioIdAsync(Guid ScenarioId)
        {
            var rules = await _getRuleRepository.GetEventsByScenarioIdAsync(ScenarioId);
            var resp = rules.Select(rule=> _ruleAdapter.ToRuleRequest(rule)).ToList();
            return resp;
        }

        public async Task<IEnumerable<RuleRequest?>> GetApisByScenarioIdAsync(Guid ScenarioId)
        {
            var rules = await _getRuleRepository.GetApiByScenarioIdAsync(ScenarioId);
            var resp = rules.Select(rule=> _ruleAdapter.ToRuleRequest(rule)).ToList();
            return resp;
        }

        public async Task<IEnumerable<RuleRequest?>> GetSchedulesByScenarioIdAsync(Guid ScenarioId)
        {
            var rules = await _getRuleRepository.GetSchedulesByScenarioIdAsync(ScenarioId);
            var resp = rules.Select(rule=> _ruleAdapter.ToRuleRequest(rule)).ToList();
            return resp;
        }


        //Get using id
        public async Task<RuleRequest?> GetRuleByIdAsync(Guid id)
        {
            var rule = await _ruleRepository.GetByIdAsync(id);
            if(rule == null){
                return null;
            }
            return _ruleAdapter.ToRuleRequest(rule);
        }

        //Get all the rules 
        public async Task<IEnumerable<RuleRequest>> GetAllRulesAsync()
        {
            var rules = await _ruleRepository.GetAllAsync();
            var resp = rules.Select(rule=> _ruleAdapter.ToRuleRequest(rule)).ToList();
            return resp;
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
                    List<RuleRequest>? newRule = JsonConvert.DeserializeObject<List<RuleRequest>>(fileContents);
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
                    // Change DateTime to 1st of Jan
                    DateTime jan1st = new DateTime(existingRule.StartTime.Value.Year, 1, 1);
                    DateTime jan2nd = jan1st.AddDays(1);

                    existingRule.StartTime = new DateTime(jan1st.Year, jan1st.Month, jan1st.Day, existingRule.StartTime.Value.Hour, existingRule.StartTime.Value.Minute, existingRule.StartTime.Value.Second);
                    existingRule.EndTime = existingRule.StartTime.Value.TimeOfDay < existingRule.EndTime.Value.TimeOfDay ?
                                            new DateTime(jan1st.Year, jan1st.Month, jan1st.Day, existingRule.EndTime.Value.Hour, existingRule.EndTime.Value.Minute, existingRule.EndTime.Value.Second) :
                                            new DateTime(jan2nd.Year, jan2nd.Month, jan2nd.Day, existingRule.EndTime.Value.Hour, existingRule.EndTime.Value.Minute, existingRule.EndTime.Value.Second);

                    // Do the same for ruleReq
                    ruleReq.StartTime = new DateTime(jan1st.Year, jan1st.Month, jan1st.Day, ruleReq.StartTime.Value.Hour, ruleReq.StartTime.Value.Minute, ruleReq.StartTime.Value.Second);
                    ruleReq.EndTime = ruleReq.StartTime.Value.TimeOfDay < ruleReq.EndTime.Value.TimeOfDay ?
                                        new DateTime(jan1st.Year, jan1st.Month, jan1st.Day, ruleReq.EndTime.Value.Hour, ruleReq.EndTime.Value.Minute, ruleReq.EndTime.Value.Second) :
                                        new DateTime(jan2nd.Year, jan2nd.Month, jan2nd.Day, ruleReq.EndTime.Value.Hour, ruleReq.EndTime.Value.Minute, ruleReq.EndTime.Value.Second);

                    bool timeOverlap = (
    // Both rules are on the same date
    (existingRule.StartTime?.Date == ruleReq.StartTime?.Date && existingRule.EndTime?.Date == ruleReq.EndTime?.Date) && (
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
    )
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
