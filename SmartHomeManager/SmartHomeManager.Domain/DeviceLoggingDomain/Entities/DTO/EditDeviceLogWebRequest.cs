using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO
{
    public class EditDeviceLogWebRequest
    {
        public DateTime DateLogged { get; set; }

        public int DeviceEnergyUsage { get; set; }

        public int DeviceActivity { get; set; }

       
    }
}
