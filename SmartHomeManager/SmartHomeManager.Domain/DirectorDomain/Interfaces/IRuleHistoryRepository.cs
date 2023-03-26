using SmartHomeManager.Domain.DirectorDomain.Entities;

namespace SmartHomeManager.Domain.DirectorDomain.Interfaces
{
    public interface IRuleHistoryRepository
    {
        public Task<bool> AddAsync(RuleHistory rh);
        public Task<IEnumerable<RuleHistory>> GetAllAsync();
        public Task<RuleHistory?> GetByRuleIdAsync(Guid ruleId);

        public Task<int> GetRuleIndexLimitAsync();

        public Task<int> CountRuleAsync();
    }
}
