using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Services
{
    public class RuleList : IRuleList
    {
        public List<Rule> ruleList;
        public RuleList(List<Rule> ruleList) {
            this.ruleList = ruleList;
        }

        public IRuleList Clone()
        {
            List<Rule> cloneList = ruleList.ConvertAll( r => new Rule
            {
                RuleId = r.RuleId,
                ScenarioId = r.ScenarioId,
                ConfigurationKey = r.ConfigurationKey,
                ConfigurationValue = r.ConfigurationValue,
                ActionTrigger = r.ActionTrigger,
                RuleName = r.RuleName,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                DeviceId = r.DeviceId,
                APIKey = r.APIKey,
                ApiValue = r.ApiValue
            }).ToList();
            RuleList clone = new RuleList(cloneList);
            return clone;
        }

        public List<Rule> getRuleList()
        {
            return ruleList;
        }

        public void replaceRuleList(List<Rule> ruleList)
        {
            this.ruleList = ruleList;
        }
    }
}
