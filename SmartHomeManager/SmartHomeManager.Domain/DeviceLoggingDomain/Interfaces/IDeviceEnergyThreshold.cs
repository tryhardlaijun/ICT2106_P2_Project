using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces
{
    public interface IDeviceEnergyThreshold
    {
        Task<DeviceLog> DeviceLimitExceeded(Guid deviceId, double energyUsage);

    }
}
