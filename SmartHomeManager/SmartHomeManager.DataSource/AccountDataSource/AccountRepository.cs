using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Entities;

namespace SmartHomeManager.DataSource.AccountDataSource
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(Account account)
        {
            // create account
            await _dbContext.Accounts.AddAsync(account);
            
            return true;
        }

        public Task<bool> DeleteAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            // get all accounts
            return await _dbContext.Accounts.ToListAsync();

            /*throw new NotImplementedException();*/
        }

        public Task<Account?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<int> SaveAsync()
        {
            int result = await _dbContext.SaveChangesAsync();

            return result;
        }

        public Task<bool> UpdateAsync(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
