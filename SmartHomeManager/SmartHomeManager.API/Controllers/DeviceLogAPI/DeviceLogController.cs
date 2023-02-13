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

namespace SmartHomeManager.API.Controllers.DeviceLogAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceLogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DeviceLogReadService _logReadService;
     //   private readonly DeviceLogWriteService _logWriteService;


        public DeviceLogController(IDeviceLogRepository deviceLogRepository, IProfileService profileService)
        {
            _logReadService = new DeviceLogReadService(deviceLogRepository, profileService);
           // _logWriteService = new DeviceLogWriteService(deviceLogRepository);
            
        }



        // GET: api/DeviceLogs
        [HttpGet]
        public async Task<ActionResult<DeviceLog>> GetAllDeviceLogs(Guid id)
        {
            return Ok(await _logReadService.GetAllDeviceLogs());
        }

        // PUT: api/DeviceLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceLog(Guid id, DeviceLog deviceLog)
        {
            if (id != deviceLog.LogId)
            {
                return BadRequest();
            }

            _context.Entry(deviceLog).State = EntityState.Modified;

 

            return NoContent();
        }

        // POST: api/DeviceLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeviceLog>> PostDeviceLog(DeviceLog deviceLog)
        {
            if (_context.DeviceLogs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DeviceLogs'  is null.");
            }
            _context.DeviceLogs.Add(deviceLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeviceLog", new { id = deviceLog.LogId }, deviceLog);
        }

        // DELETE: api/DeviceLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceLog(Guid id)
        {
            if (_context.DeviceLogs == null)
            {
                return NotFound();
            }
            var deviceLog = await _context.DeviceLogs.FindAsync(id);
            if (deviceLog == null)
            {
                return NotFound();
            }

            _context.DeviceLogs.Remove(deviceLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
