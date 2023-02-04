using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;



namespace SmartHomeManager.DataSource.RulesDataSource
{
	public class RuleRepository : IGenericRepository<Rule>
	{
        private readonly ApplicationDbContext _applicationDbContext;

        public RuleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddAsync(Rule rule)
        {
            try
            {
                _applicationDbContext.Rules.Add(rule);
                await _applicationDbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public Task<bool> DeleteAsync(Rule entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                var rule = await _applicationDbContext.Rules.FindAsync(id);
                if(rule != null)
                {
                    _applicationDbContext.Rules.Remove(rule);
                    await _applicationDbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }           
        }

        public async Task<IEnumerable<Rule>> GetAllAsync()
        {
            var result = await _applicationDbContext.Rules.ToListAsync();

            return result;
        }

        public async Task<Rule?> GetByIdAsync(Guid id)
        {
            try
            {
                var rule = await _applicationDbContext.Rules.FindAsync(id);
                return rule;
            }
            catch
            {
               return  null;
            }
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Rule entity)
        {
            throw new NotImplementedException();
        }
    }
}

