using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Services
{
    public class RuleServices
    {
        private readonly IGenericRepository<Rule> _ruleRepository;

        public RuleServices(IGenericRepository<Rule> ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }

        public async Task<IEnumerable<Rule>> GetAllRulesAsync()
        {
            return await _ruleRepository.GetAllAsync();
        }
    }
}
