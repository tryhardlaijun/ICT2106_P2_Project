using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.EnergyProfileDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.EnergyProfileDomain.Interfaces
{
    public interface IEnergyProfileRepository
    {
        public Task<bool> UpdateConfigValueAsync(Guid accountId, int configValue);
        public Task<EnergyProfile?> GetByIdAsync(Guid accountId);
        public Task<IEnumerable<EnergyProfile>> GetAllAsync();
        public Task<bool> UpdateAsync(EnergyProfile energyProfile);
        public Task<bool> AddAsync(EnergyProfile energyProfile);

    }
}
