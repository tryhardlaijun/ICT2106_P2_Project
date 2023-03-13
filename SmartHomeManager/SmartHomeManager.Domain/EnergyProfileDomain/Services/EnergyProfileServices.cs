using SmartHomeManager.Domain.AccountDomain.Entities;
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

        public async Task<IEnumerable<EnergyProfile>> GetAllEnergyProfilesAsync()
        {
            return await _energyProfileRepository.GetAllAsync();
        }

        public async Task<EnergyProfile> GetEnergyProfileAsync(Guid accountId)
        {
            return await _energyProfileRepository.GetByIdAsync(accountId);
        }

        public async Task<bool> PostEnergyProfileAsync(EnergyProfile energyProfile)
        {
            return await _energyProfileRepository.AddAsync(energyProfile);
        }

        public async Task<bool> PutEnergyProfileAsync(EnergyProfile energyProfile)
        {
            return await _energyProfileRepository.UpdateAsync(energyProfile);
        }

        public int getRevisedConfigValue(Guid deviceID, string configurationKey, int configurationValue)
        {
            // Simulate using function from EnergyEfficiency Analytics
            double efficiencyIndex = GetDeviceEnergyEfficiency(deviceID);

            //Simulate another function from IDeviceInfoService

            /*
            // Hardcoded accountId
            Guid accountId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            EnergyProfile energyProfile = await _energyProfileRepository.GetByIdAsync(accountId);
            if (energyProfile == null)
            {
                return -1;
            }
            int newConfigurationValue = calculateNewConfigValue(efficiencyIndex, energyProfile.ConfigurationValue, configurationValue, );

            return newConfigurationValue;
            */
            return configurationValue;
        }

        // Simulate implementation of function from EnergyEfficiency Analytics
        private double GetDeviceEnergyEfficiency(Guid deviceId)
        {
            Random random = new Random();
            double randomNumber = random.NextDouble() * 100.0;
            return randomNumber;
        }

        private int calculateNewConfigValue(double receivedEnergyEff, int energyProfileValue, int configurationValue, List<int> configurationValueRange)
        {
            throw new NotImplementedException();
        }
    }
}
