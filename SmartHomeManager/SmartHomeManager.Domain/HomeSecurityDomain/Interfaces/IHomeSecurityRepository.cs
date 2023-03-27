using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Interfaces
{
    public interface IHomeSecurityRepository<T> where T : class
    {
        public Task<bool> AddAsync(T entity);
        public Task<bool> UpdateAsync(T entity);
        public Task<T?> GetByAccountIdAsync(Guid accountId);
        public Task<bool> SaveAsync();
    }
}
