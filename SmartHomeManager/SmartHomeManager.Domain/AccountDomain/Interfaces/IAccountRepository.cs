using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
    public interface IAccountRepository
    {
        public Task<bool> AddAsync(Account account);
        public Task<Account?> GetByIdAsync(Guid id);
        public Task<IEnumerable<Account>> GetAllAsync();
        public Task<Account?> GetAccountByEmailAsync(string email);
        public Task<bool> IsEmailUnique(string email);
        public Task<bool> UpdateAsync(Account account);
        public Task<bool> DeleteAsync(Account account);
        public Task<bool> DeleteByIdAsync(Guid id);
        public Task<int> SaveAsync();
        public Task<IEnumerable<Device?>> GetDevicesByProfileId(Guid id);

    }
}
