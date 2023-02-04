using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.DeviceLogDataSource
{
    public class DeviceLogRepository: iDeviceLogRepository
    {
        protected readonly ApplicationDbContext _db;

        protected DbSet<DeviceLog> _dbSet;
        public DeviceLogRepository(ApplicationDbContext db) {
            _db = db;

            this._dbSet = db.Set<DeviceLog>();
        }

        public IEnumerable<DeviceLog> GetAll()
        {
            IQueryable<DeviceLog> query = _dbSet;
            return query.ToList();
        }

        public DeviceLog GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDeviceLog(IEnumerable<DeviceLog> entities)
        {
            throw new NotImplementedException();
        }
    }
}
