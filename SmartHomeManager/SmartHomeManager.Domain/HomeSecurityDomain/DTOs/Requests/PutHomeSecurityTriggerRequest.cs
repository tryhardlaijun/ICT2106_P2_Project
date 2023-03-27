using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.HomeSecurityDomain.DTOs.Requests
{
    public class PutHomeSecurityTriggerRequest
    {
        public string DeviceGroup { get; set; } 
        public string ConfigurationKey { get; set; }
        public int ConfigurationValue { get; set; }
    }
}
