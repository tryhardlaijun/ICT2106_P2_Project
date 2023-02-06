using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Services
{
    public class RegisterDeviceService
    {
        private readonly IGenericRepository<Device> _deviceRepository;
        private readonly IGenericRepository<DeviceType> _deviceTypeRepository;

        public RegisterDeviceService(IGenericRepository<Device> deviceRepository, IGenericRepository<DeviceType> deviceTypeRepository) 
        { 
            _deviceRepository = deviceRepository;
            _deviceTypeRepository = deviceTypeRepository;
        }

        public async Task<IEnumerable<DeviceType>> GetAllDevicesTypeAsync() 
        {
            return await _deviceTypeRepository.GetAllAsync();
        }

        public async Task<bool> RegisterDeviceAsync(string deviceName, string deviceBrand, string deviceModel, string deviceTypeName, Guid accountId, Guid profileId)
        {
            Device device = new()
            {
                DeviceName = deviceName,
                DeviceBrand = deviceBrand,
                DeviceModel = deviceModel,
                DeviceTypeName = deviceTypeName,
                AccountId = accountId,
                ProfileId = profileId,
            };

            return await _deviceRepository.AddAsync(device);
        }

        public async Task<bool> AddDeviceTypeAsync(DeviceType deviceType)
        {
            return await _deviceTypeRepository.AddAsync(deviceType);
        }
    }
}
