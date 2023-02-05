using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.RuleHistoryDataSource
{
    public class RuleHistoryRepository : IGenericRepository<RuleHistory>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RuleHistoryRepository(ApplicationDbContext applicationDbContext) { 
            _applicationDbContext= applicationDbContext;
        }
        
        public async Task<bool> AddAsync(RuleHistory rh)
        {
            // Add task
            try
            {
                await _applicationDbContext.RuleHistories.AddAsync(rh);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            } catch
            {
                return false;
            }
        }

        public Task<bool> DeleteAsync(RuleHistory entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RuleHistory>> GetAllAsync()
        {
            return await _applicationDbContext.RuleHistories.ToListAsync();            
        }

        public Task<RuleHistory?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(RuleHistory entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            return _applicationDbContext.RuleHistories.CountAsync();
        }

    }
}
