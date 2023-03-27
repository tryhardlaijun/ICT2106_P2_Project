using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.RuleHistoryDataSource
{
    public class RuleHistoryRepository : IRuleHistoryRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RuleHistoryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddAsync(RuleHistory rh)
        {
            // Add task
            try
            {
                await _applicationDbContext.RuleHistories.AddAsync(rh);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<RuleHistory>> GetAllAsync()
        {
            return await _applicationDbContext.RuleHistories.ToListAsync();
        }

        public async Task<RuleHistory?> GetByRuleIdAsync(Guid ruleId)
        {
            IEnumerable<RuleHistory> rh = await _applicationDbContext.RuleHistories.Where(r => r.RuleId == ruleId).ToListAsync();
            if (rh.Any()) return rh.OrderByDescending(r => r.RuleIndex).First();
            return null;
        }

        public async Task<int> GetRuleIndexLimitAsync()
        {
            return await _applicationDbContext.RuleHistories.MaxAsync(r => r.RuleIndex);
        }
        public async Task<int> CountRuleAsync()
        {
            return await _applicationDbContext.RuleHistories.CountAsync();
        }
    }
}
