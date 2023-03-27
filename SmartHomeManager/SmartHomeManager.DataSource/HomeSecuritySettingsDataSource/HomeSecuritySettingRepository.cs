using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.HomeSecuritySettingsDataSource
{
    public class HomeSecuritySettingRepository : IGenericRepository<HomeSecuritySetting>
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

        public async Task<bool> DeleteAsync(HomeSecuritySetting entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HomeSecuritySetting>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<HomeSecuritySetting?> GetByIdAsync(Guid homeSecuritySettingId)
        {
            return await _applicationDbContext.HomeSecuritySettings.Where(r => r.HomeSecuritySettingId == homeSecuritySettingId).LastAsync();
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
        
        public async Task<HomeSecuritySetting?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
