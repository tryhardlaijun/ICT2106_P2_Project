using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.Common;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
    public class AutoClashService : IAutomationClash
    {
        private readonly IGenericRepository<Rule> _ruleRepository;

        public AutoClashService(IGenericRepository<Rule> ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }

        public async Task<IEnumerable<Rule>> GetAllRules()
        {
            return await _ruleRepository.GetAllAsync();
        }

        public async Task<bool> CheckForScheduleClash(Rule newRule)
        {
            // Get all rules from the database
            var allRules = await _ruleRepository.GetAllAsync();

            // Check if the new rule's start and end times overlap with any existing rules
            foreach (var existingRule in allRules)
            {
                // Skip comparing the new rule with itself
                if (existingRule.RuleId == newRule.RuleId)
                    continue;

                // Check if the rules have the same device and configuration key
                if (existingRule.DeviceId == newRule.DeviceId
                    && existingRule.ConfigurationKey == newRule.ConfigurationKey)
                {
                    // Check if the rules overlap in time
                    if (existingRule.StartTime.HasValue && newRule.StartTime.HasValue
                        && existingRule.StartTime.Value <= newRule.EndTime.Value
                        && existingRule.EndTime.HasValue && newRule.EndTime.HasValue
                        && existingRule.EndTime.Value >= newRule.StartTime.Value)
                    {
                        // There is a schedule clash
                        return true;
                    }
                }
            }

            // No clashes found
            return false;
        }

    }
}

