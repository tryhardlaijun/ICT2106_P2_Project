using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Services
{
    public class GetDeviceService
    {
        private readonly iDeviceLogRepository deviceLogRepository;

        public GetDeviceService(iDeviceLogRepository deviceLogRepository)
        {
            this. deviceLogRepository = deviceLogRepository;
        }

    }
}
