using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.DeviceDomain.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities.DTOs;

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
        public async Task RegisterDevice([FromBody] DeviceWebRequest deviceWebRequest)
        {
            await _registerDeviceService.RegisterDeviceAsync(deviceWebRequest.DeviceName, deviceWebRequest.DeviceBrand, deviceWebRequest.DeviceModel, deviceWebRequest.DeviceTypeName, deviceWebRequest.AccountId);
        }

        // POST api/<RegisterDeviceController>/AddDeviceType
        [HttpPost("AddDeviceType")]
        public async Task AddDeviceType([FromBody] DeviceType deviceType)
        {
            await _registerDeviceService.AddDeviceTypeAsync(deviceType);   
        }
    }
}
