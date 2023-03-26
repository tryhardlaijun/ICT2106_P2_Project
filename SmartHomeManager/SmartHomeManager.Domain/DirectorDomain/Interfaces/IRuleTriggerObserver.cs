using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Interfaces
{
    public interface IRuleTriggerObserver
    {
        public void Update(Guid deviceId, string configKey, int configVal);
    }
}
