using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
	public interface IRuleAdapter
	{
        public RuleRequest ToRuleRequest(Rule rule);
        public Rule ToRule(RuleRequest ruleRequest);
    }
}

