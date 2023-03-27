using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using SmartHomeManager.Domain.HomeSecurityDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.HomeSecuritySettingsDataSource
{
    public class HomeSecuritySettingRepository : IHomeSecuritySettingRepository<HomeSecuritySetting>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeSecuritySettingRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddAsync(HomeSecuritySetting homeSecuritySetting)
        {
            try
            {
                await _applicationDbContext.HomeSecuritySettings.AddAsync(homeSecuritySetting);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<HomeSecuritySetting>> GetAllAsync()
        {
            return await _applicationDbContext.HomeSecuritySettings.Include(hs => hs.HomeSecurityDeviceDefinition).ToListAsync();
        }

        public async Task<IEnumerable<HomeSecuritySetting?>> GetByHomeSecurityIdAsync(Guid homeSecurityId)
        {
            return await _applicationDbContext.HomeSecuritySettings.Where(r => r.HomeSecurityId == homeSecurityId).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(HomeSecuritySetting homeSecuritySetting)
        {
            try
            {
                _applicationDbContext.Update(homeSecuritySetting);
                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }
        
    }
}
