using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.DataSource.DeviceDataSource
{
    public class DeviceRepository : IGenericRepository<Device>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeviceRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddAsync(Device device)
        {
            try
            {
                await _applicationDbContext.Devices.AddAsync(device);
                await SaveAsync();

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Device device)
        {
            try
            {
                _applicationDbContext.Devices.Remove(device);
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
                Device? device = await GetByIdAsync(id);

                if (device is null)
                {
                    return false;
                }

                _applicationDbContext.Devices.Remove(device);
                await SaveAsync();

                return true;
            }
            catch 
            {
                return false; 
            }
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await _applicationDbContext.Devices.ToListAsync();
        }

        public async Task<Device?> GetByIdAsync(Guid id)
        {
            return await _applicationDbContext.Devices.FindAsync(id);    
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

        public async Task<bool> UpdateAsync(Device device)
        {
            try
            {
                _applicationDbContext.Devices.Update(device);
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
