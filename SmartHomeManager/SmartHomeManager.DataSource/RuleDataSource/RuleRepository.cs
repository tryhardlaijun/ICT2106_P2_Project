using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.RuleDataSource
{
    
    public class RuleRepository: IGenericRepository<Rule>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RuleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<bool> AddAsync(Rule entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Rule entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Rule>> GetAllAsync()
        {
            return await _applicationDbContext.Rules.ToListAsync();
        }

        public Task<Rule?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
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
