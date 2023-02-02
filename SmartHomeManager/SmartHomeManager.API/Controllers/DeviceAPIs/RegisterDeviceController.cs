using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.DataSource.DeviceDataSource.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHomeManager.API.Controllers.DeviceAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterDeviceController : ControllerBase
    {
        private readonly RegisterDeviceService _registerDeviceService;

        public RegisterDeviceController(IGenericRepository<Device> deviceRepository, IGenericRepository<DeviceType> deviceTypeRepository) 
        {
            _registerDeviceService = new(deviceRepository, deviceTypeRepository);
        }

        // GET api/<RegisterDeviceController>/GetAllDeviceTypes
        [HttpGet("GetAllDeviceTypes")]
        public async Task<IEnumerable<string>> GetAllDeviceTypes()
        {
            IEnumerable<DeviceType> deviceTypes = await _registerDeviceService.GetAllDevicesTypeAsync();
            return deviceTypes.Select(deviceType => deviceType.DeviceTypeName);    
        }

        // POST api/<RegisterDeviceController>/RegisterDevice
        [HttpPost("RegisterDevice")]
        public async Task RegisterDevice([FromBody] string deviceName, string deviceBrand, string deviceModel, string deviceTypeName, Guid accountId, Guid profileId)
        {
            await _registerDeviceService.RegisterDeviceAsync(deviceName, deviceBrand, deviceModel, deviceTypeName, accountId, profileId);   
        }

        // POST api/<RegisterDeviceController>/AddDeviceType
        [HttpPost("AddDeviceType")]
        public async Task AddDeviceType([FromBody] string deviceTypeName)
        {
            await _registerDeviceService.AddDeviceTypeAsync(deviceTypeName);   
        }
    }
}
