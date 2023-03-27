using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.HistoryDataSource
{
    public class HistoryRepository : IGenericRepository<History>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public HistoryRepository(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<bool> AddAsync(History history)
        {
            try
            {
                await _applicationDbContext.Histories.AddAsync(history);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            } catch
            {
                return false;
            }
        }

        public Task<bool> DeleteAsync(History entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<History>> GetAllAsync()
        {
            return await _applicationDbContext.Histories.Include(r => r.RuleHistory).Include(p => p.Profile).ToListAsync();
        }

        public Task<History?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(History entity)
        {
            throw new NotImplementedException();
        }
        
        public async Task<History?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
        
    }
}
