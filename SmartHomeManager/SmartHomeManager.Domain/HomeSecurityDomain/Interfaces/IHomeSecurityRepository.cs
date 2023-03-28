using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Interfaces
{
    public interface IHomeSecurityRepository
    {
        public Task<bool> AddAsync(HomeSecurity hs);
        public Task<bool> UpdateAsync(HomeSecurity hs);
        public Task<HomeSecurity?> GetByAccountIdAsync(Guid accountId);
        public Task<bool> SaveAsync();
    }
}
