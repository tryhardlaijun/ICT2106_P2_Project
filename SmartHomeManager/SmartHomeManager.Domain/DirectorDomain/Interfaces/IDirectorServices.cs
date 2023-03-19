using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartHomeManager.Domain.DirectorDomain.Interfaces
{
    public interface IDirectorServices
    {
        void executeSecurityProtocol(Guid accountId, bool target, HomeSecurityDeviceDefinition homeSecurityDeviceDefinition);
    }
}
