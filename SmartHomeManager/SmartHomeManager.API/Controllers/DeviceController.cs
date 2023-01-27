using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.Entities;
using SmartHomeManager.Domain.Interfaces.Services;

namespace SmartHomeManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        // GET: api/<DeviceController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DeviceController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            Device? device = await _deviceService.GetByIdAsync(id);
            return device?.Name ?? string.Empty;
        }
    }
}
