using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;

namespace SmartHomeManager.DataSource.AccountDataSource
{
    public class MockAccountRepository : IGenericRepository<Account>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public MockAccountRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext= applicationDbContext;    
        }

        public async Task<bool> AddAsync(Account entity)
        {
            try
            {
                // Attempt to add entity to db, check if operation was successful.
                await _applicationDbContext.Accounts.AddAsync(entity);
                bool success = await _applicationDbContext.SaveChangesAsync() > 0;
                return success;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> DeleteAsync(Account entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _applicationDbContext.Accounts.ToListAsync();
        }

        public async Task<Account?> GetByIdAsync(Guid id)
        {
            return await _applicationDbContext.Accounts.FindAsync(id);
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Account entity)
        {
            throw new NotImplementedException();
        }
        
    }
}
