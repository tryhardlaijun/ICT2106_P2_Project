using SmartHomeManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.EnergyProfileDomain.Interfaces
{
    public interface IEnergyProfileRepository<T> : IGenericRepository<T> where T : class
    {
        public Task<bool> UpdateConfigValueAsync(Guid accountId, int configValue);
    }
}
