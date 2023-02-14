using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.EnergyProfileDomain.Interfaces
{
    public interface IEnergyProfileServices
    {
        public int getRevisedConfigValue(Guid deviceID, String configurationKey, int configurationValue);
    }
}
