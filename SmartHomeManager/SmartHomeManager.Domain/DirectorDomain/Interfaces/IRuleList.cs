using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Interfaces
{
    public interface IRuleList
    {        
        public IRuleList Clone();
        public void replaceRuleList(List<Rule> ruleList);
        public List<Rule> getRuleList();
    }
}
