using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
    public class RuleAdapter: IRuleAdapter
    {
        public RuleRequest ToRuleRequest(Rule rule)
        {
            Console.WriteLine(rule);
            return new RuleRequest
            {
                RuleId = rule.RuleId,
                ScenarioId = rule.ScenarioId,
                ConfigurationKey = rule.ConfigurationKey,
                ConfigurationValue = rule.ConfigurationValue,
                ActionTrigger = rule.ActionTrigger,
                RuleName = rule.RuleName,
                StartTime = (rule.StartTime != null) ? Convert.ToDateTime(rule.StartTime) : null,
                EndTime = (rule.EndTime != null) ? Convert.ToDateTime(rule.EndTime) : null,
                DeviceId = rule.DeviceId,
                APIKey = rule.APIKey,
                ApiValue = rule.ApiValue,
            };
        }

        public Rule ToRule(RuleRequest ruleRequest)
        {
            return new Rule
            {
                RuleId = ruleRequest.RuleId,
                ScenarioId = ruleRequest.ScenarioId,
                ConfigurationKey = ruleRequest.ConfigurationKey,
                ConfigurationValue = ruleRequest.ConfigurationValue,
                ActionTrigger = ruleRequest.ActionTrigger,
                RuleName = ruleRequest.RuleName,
                StartTime = (ruleRequest.StartTime != null) ? Convert.ToDateTime(ruleRequest.StartTime) : null,
                EndTime = (ruleRequest.EndTime != null) ? Convert.ToDateTime(ruleRequest.EndTime) : null,
                DeviceId = ruleRequest.DeviceId,
                APIKey = ruleRequest.APIKey,
                ApiValue = ruleRequest.ApiValue,
            };
        }
    }

}