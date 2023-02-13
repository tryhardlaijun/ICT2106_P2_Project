using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Services;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Mocks;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.API.Controllers.DeviceLogAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceLogController : ControllerBase
    {

        private readonly DeviceLogReadService _logReadService;
        private readonly DeviceLogWriteService _logWriteService;


        public DeviceLogController(IDeviceLogRepository deviceLogRepository, IProfileService profileService, IDeviceWattsService deviceWattsService)
        {
           _logReadService = new DeviceLogReadService(deviceLogRepository, profileService, deviceWattsService);
            _logWriteService = new DeviceLogWriteService(deviceLogRepository);
            
        }



        // GET: api/DeviceLogs
        [HttpGet]
        public async Task<ActionResult<DeviceLog>> GetAllDeviceLogs(Guid id)
        {
            return Ok(await _logReadService.GetAllDeviceLogs());
        }


        /*        // PUT: api/DeviceLogs/5
                // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
                [HttpPut("{id}")]
                public async Task<IActionResult<GetDeviceLogWebRequest> GetDeviceLog(DateTime date, DateTime startTime, DateTime endTime)
                {
                     var result = await _logReadService.GetDeviceLogByDateAndTime(date, startTime, endTime);
                     if (result == null) return NotFound();
                     return result; 
                }
        */

        // this is update from switching off device
        // PUT: api/DeviceLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceLog(DateTime date, Guid deviceId, EditDeviceLogWebRequest deviceLogWebRequest)
        {
            var res = await _logReadService.GetDeviceLogByDate(date, deviceId, true);

            if (res == null) return BadRequest();

            var endTime = deviceLogWebRequest.EndTime;
            // switching the state
            var deviceState = false;
            await _logWriteService.UpdateDeviceLog((DateTime)endTime, deviceState);

            return NoContent();

        }


        // GET: api/Rooms/GetDevicesRelatedToRoom/profileId
        [HttpGet("GetDevicesInProfile/{profileId}")]
        public ActionResult<IEnumerable<Device>> GetDevicesInRoom(Guid profileId)
        {
            var result = _logReadService.GetAllDevicesInProfile(profileId);
            if (!result.Any()) return NotFound();

            return Ok(result);
        }


        // POST: api/DeviceLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetDeviceLogWebRequest>> PostDeviceLog(CreateDeviceLogWebRequest deviceLogWebRequest )
        {
        var resp = await _logWriteService.AddDeviceLog(deviceLogWebRequest.DeviceId);
        return Ok(resp);
        }


    }
}
