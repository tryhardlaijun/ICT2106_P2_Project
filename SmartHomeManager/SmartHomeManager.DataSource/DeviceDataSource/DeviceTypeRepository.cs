using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.DeviceDataSource
{
    public class DeviceTypeRepository : IGenericRepository<DeviceType>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeviceTypeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddAsync(DeviceType deviceType)
        {
            try
            {
                await _applicationDbContext.DeviceTypes.AddAsync(deviceType);
                await SaveAsync();

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(DeviceType deviceType)
        {
            try
            {
                _applicationDbContext.DeviceTypes.Remove(deviceType);
                await SaveAsync();

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                DeviceType? deviceType = await GetByIdAsync(id);

                if (deviceType is null)
                {
                    return false;
                }

                _applicationDbContext.DeviceTypes.Remove(deviceType);
                await SaveAsync();

                return true;
            }
            catch 
            {
                return false; 
            }
        }

        public async Task<IEnumerable<DeviceType>> GetAllAsync()
        {
            return await _applicationDbContext.DeviceTypes.ToListAsync();
        }

        public async Task<DeviceType?> GetByIdAsync(Guid id)
        {
            return await _applicationDbContext.DeviceTypes.FindAsync(id);    
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

        public async Task<bool> UpdateAsync(DeviceType deviceType)
        {
            try
            {
                _applicationDbContext.DeviceTypes.Update(deviceType);
                await SaveAsync();

                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
