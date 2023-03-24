using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.HomeSecurityDomain.DTOs.Requests
{
    public class PutSecuritySettingsEnabledRequest
    {
        public string DeviceGroup { get; set; }
        public bool Enabled { get; set; }
    }
}
