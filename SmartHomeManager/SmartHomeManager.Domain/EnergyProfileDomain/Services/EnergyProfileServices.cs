using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.EnergyProfileDomain.Entities;
using SmartHomeManager.Domain.EnergyProfileDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.EnergyProfileDomain.Services
{
    public class EnergyProfileServices : IEnergyProfileServices
    {
        private readonly IGenericRepository<EnergyProfile> _energyProfileRepository;

        public EnergyProfileServices(IGenericRepository<EnergyProfile> energyProfileRepository)
        {
            _energyProfileRepository = energyProfileRepository;
        }

        public async Task<IEnumerable<EnergyProfile>> GetAllRulesAsync()
        {
            return await _energyProfileRepository.GetAllAsync();
        }

        public int getRevisedConfigValue(Guid deviceID, string configurationKey, int configurationValue)
        {
            return ++configurationValue;
        }

        private int calculateNewConfigValue(float receivedEnergyEff, int energyProfileValue, int configurationValue, List<int> configurationValueRange)
        {
            throw new NotImplementedException();
        }
    }
}
