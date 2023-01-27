using SmartHomeManager.DataSource.Contexts;
using SmartHomeManager.Domain.Entities;
using SmartHomeManager.Domain.Interfaces;

namespace SmartHomeManager.DataSource.Repositories
{
    public class DeviceRepository : IGenericRepository<Device>
    {
        //private readonly DeviceDbContext _deviceDbContext;

        public DeviceRepository(/*DeviceDbContext deviceDbContext*/)
        {
            //_deviceDbContext = deviceDbContext;
        }

        public Task<bool> AddAsync(Device entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Device?> GetByIdAsync(int id)
        {
            Device? test = new(0, "test device");
            await Task.Delay(0);
            return test;
            //return await _deviceDbContext.Devices.FindAsync(id);
        }

        public Task<IEnumerable<Device>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Device entity) 
        { 
            throw new NotImplementedException(); 
        }

        public Task<bool> DeleteAsync(Device entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
