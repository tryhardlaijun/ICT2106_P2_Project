using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
    public class TroubleshooterServices : ITroubleshooterServices
    {
        public void Update(Guid deviceId, string configKey, int configVal)
        {
            Console.WriteLine("Updating Troubleshooter Service");
        }
    }
}
