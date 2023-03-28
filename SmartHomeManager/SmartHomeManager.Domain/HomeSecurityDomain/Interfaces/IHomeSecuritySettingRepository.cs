using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Interfaces
{
    public interface IHomeSecuritySettingRepository
    {
        public Task<bool> AddAsync(HomeSecuritySetting hss);
        public Task<IEnumerable<HomeSecuritySetting?>> GetByHomeSecurityIdAsync(Guid homeSecurityId);
        public Task<IEnumerable<HomeSecuritySetting>> GetAllAsync();
        public Task<bool> UpdateAsync(HomeSecuritySetting hss);
    }
}
