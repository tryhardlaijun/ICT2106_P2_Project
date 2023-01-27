using SmartHomeManager.Domain.Entities;
using SmartHomeManager.Domain.Interfaces;
using SmartHomeManager.Domain.Interfaces.Services;

namespace SmartHomeManager.DataSource.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IGenericRepository<Device> _deviceRepository;

        public DeviceService(IGenericRepository<Device> deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public Task<bool> Add(Device entity)
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

        public Task<IEnumerable<Device>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Device?> GetByIdAsync(int id)
        {
            Device? device = await _deviceRepository.GetByIdAsync(id);

            return device;
            //throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Device entity)
        {
            throw new NotImplementedException();
        }
    }
}
