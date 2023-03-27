using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Interfaces
{
    public interface IHomeSecurityServices
    {
        void processEventAsync(Guid accountID, String deviceGroup, String configurationKey, int configurationValue);
    }
}
