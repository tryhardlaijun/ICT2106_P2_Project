using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Interfaces
{
    internal interface IHomeSecurityServices
    {
        List<string> getHomeSecurityCompatibleDevices();
        void processEvent(Guid accountID);
    }
}
