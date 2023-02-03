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
        private ApplicationDbContext context;

        public DeviceLogRepository(ApplicationDbContext context) {
            this.context = context;
        }

        public void DeleteDeviceLog(Guid id)
        {
            DeviceLog deviceLog = context.DeviceLogs.Find(id);
            context.DeviceLogs.Remove(deviceLog);
        }

        public IEnumerable<DeviceLog> GetAll()
        {
            return context.DeviceLogs.ToList();
        }

        public DeviceLog GetById(Guid id)
        {
           return context.DeviceLogs.Find(id);
        }

        public void InsertDeviceLog(DeviceLog deviceLog)
        {
            context.DeviceLogs.Add(deviceLog);
        }
        public void UpdateDeviceLog(DeviceLog deviceLog)
        {
            context.Entry(deviceLog).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
