using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces
{
    public interface iDeviceLogRepository
    {
           IEnumerable<DeviceLog> GetAll();
        DeviceLog GetById(Guid id);
        void InsertDeviceLog(DeviceLog deviceLog);
        void UpdateDeviceLog(DeviceLog deviceLog);
        void DeleteDeviceLog(Guid id);
        void Save();
    }
}
