using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
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

        public IEnumerable<DeviceLog> GetAll()
        {
            IQueryable<DeviceLog> query = _dbSet;
            return query.ToList();
        }

        public async Task<DeviceLog> GetByDate(Guid id, DateTime date)
        {
            var result = await _dbSet.FindAsync(id,date);
            return result;
        }

        public void UpdateDeviceLog(IEnumerable<DeviceLog> entities)
        {
            _dbSet.Update((DeviceLog)entities);
        }

    }
}
