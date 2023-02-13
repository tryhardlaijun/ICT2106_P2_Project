using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.DeviceLogDataSource
{
    public class DeviceLogRepository: IDeviceLogRepository
    {
        private readonly ApplicationDbContext _db;
        private DbSet<DeviceLog> _dbSet;

        public DeviceLogRepository(ApplicationDbContext db) {
            _db = db;
            this._dbSet = db.Set<DeviceLog>();
        }

        public void Add(DeviceLog entity)
        {
            _dbSet.Add(entity);
        }

        public IEnumerable<DeviceLog> Get(DateTime date, DateTime startTime, DateTime endTime)
        {
            // get all logs
            var allLogs = _db.DeviceLogs.ToList();

            IEnumerable<DeviceLog> result = _db.DeviceLogs.ToList().Where(log => log.DateLogged == date && log.StartTime >= startTime && log.EndTime <= endTime) ;

            return result;
        }

        public async Task<DeviceLog?> Get(DateTime date, bool DeviceState)
        {
            var result = await _dbSet.FindAsync(date, DeviceState);
            return result;
        }

        public async Task<DeviceLog?> Get(DateTime date)
        {
            var result = await _dbSet.FindAsync(date); 
            return result;
        }

        public async Task<IEnumerable<DeviceLog>> GetAll()
        {
            IEnumerable<DeviceLog> query = await _dbSet.ToListAsync();
            return query;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Update(DeviceLog entity)
        {
            _dbSet.Update(entity);
        }
    }
}
