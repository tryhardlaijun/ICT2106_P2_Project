using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Services
{
    public class RuleHistoryServices
    {
        private readonly IGenericRepository<RuleHistory> _ruleHistoryRepository;

        public RuleHistoryServices(IGenericRepository<RuleHistory> ruleHistoryRepository)
        {
            _ruleHistoryRepository = ruleHistoryRepository;
        }

        public async Task<IEnumerable<RuleHistory>> GetAllRulesAsync()
        {
            return await _ruleHistoryRepository.GetAllAsync();
        }
    }
}
