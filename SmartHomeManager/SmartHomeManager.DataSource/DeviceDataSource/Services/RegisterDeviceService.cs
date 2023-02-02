using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.DeviceDataSource.Services
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

        public async Task<bool> CreateDeviceAsync(Device device)
        { 
            return await _deviceRepository.AddAsync(device);
        }

        public async Task<bool> AddDeviceType(DeviceType deviceType)
        {
            return await _deviceTypeRepository.AddAsync(deviceType);
        }
    }
}
