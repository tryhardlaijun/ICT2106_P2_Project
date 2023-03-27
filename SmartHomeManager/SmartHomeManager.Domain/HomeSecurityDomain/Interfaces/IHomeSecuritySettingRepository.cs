using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Interfaces
{
    public interface IHomeSecuritySettingRepository<T> where T : class
    {
        public Task<bool> AddAsync(T entity);
        public Task<IEnumerable<T?>> GetByHomeSecurityIdAsync(Guid homeSecurityId);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<bool> UpdateAsync(T entity);
    }
}
