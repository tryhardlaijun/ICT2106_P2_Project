using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
    public interface IDirectorControlDeviceService : IRuleTriggerObserver
    {
        public Task<bool> SetDeviceTypeConfiguration(Guid accountId, string deviceTypeName, string configurationKey, int configurationValue);
    }
}
