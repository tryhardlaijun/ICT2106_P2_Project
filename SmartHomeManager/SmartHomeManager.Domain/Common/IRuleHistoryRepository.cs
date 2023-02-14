using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;

namespace SmartHomeManager.DataSource.RuleHistoryDataSource
{
    /// <summary>
    /// Most repositories should extend this interface for dependency injection.
    /// </summary>
    /// <typeparam name="T">The entity that the repository will handle.</typeparam>
    public interface IRuleHistoryRepository<T> : IGenericRepository<T> where T : class
    {
        public Task<T?> GetByRuleIdAsync(Guid id);

        public Task<int> GetRuleIndexLimitAsync();

        public Task<int> CountRuleAsync();
    }
}

