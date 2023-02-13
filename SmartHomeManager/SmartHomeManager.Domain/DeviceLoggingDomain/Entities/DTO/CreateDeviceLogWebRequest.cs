using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO
{
    public class CreateDeviceLogWebRequest
    {
        public Guid DeviceId { get; set; }
    }
}
