using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.EnergyProfileDomain.Entities;
using SmartHomeManager.Domain.EnergyProfileDomain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.EnergyProfileDomain.Services
{
    public class EnergyProfileServices : IEnergyProfileServices
    {
        private readonly IEnergyProfileRepository<EnergyProfile> _energyProfileRepository;

        public EnergyProfileServices(IEnergyProfileRepository<EnergyProfile> energyProfileRepository)
        {
            _energyProfileRepository = energyProfileRepository;
        }

        public async Task<IEnumerable<EnergyProfile>> GetAllEnergyProfilesAsync()
        {
            return await _energyProfileRepository.GetAllAsync();
        }

        public async Task<EnergyProfile?> GetEnergyProfileAsync(Guid accountId)
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

        public async Task<bool> PutEnergyProfileConfigValueAsync(Guid accountId, int configValue)
        {
            return await _energyProfileRepository.UpdateConfigValueAsync(accountId, configValue);
        }

        public async Task<int> getRevisedConfigValue(Guid deviceID, string configurationKey, int configurationValue)
        {
            //blacklisted config keys
            string[] blackListedConfigKeys = new string[] { "Color", "Light" };
            if (blackListedConfigKeys.Contains(configurationKey))
            {
                return configurationValue;
            }

            // Simulate using function from EnergyEfficiency Analytics
            double efficiencyIndex = GetDeviceEnergyEfficiency(deviceID);

            // whitelisted config keys. values are min, max, direction
            Dictionary<string, List<string>> allConfigValues = new Dictionary<string, List<string>>();
            allConfigValues.Add("AC", new List<string> { "16", "32", "negative" });
            allConfigValues.Add("Fan", new List<string> { "1", "10", "positive" });
            allConfigValues.Add("Heater", new List<string> { "32", "40", "positive" });

            //Simulate another function from IDeviceInfoService
            List<int> configValues = ConfigValueRange(allConfigValues[configurationKey]);

            // Hardcoded accountId
            Guid accountId = new Guid("11111111-1111-1111-1111-111111111111");
            EnergyProfile energyProfile = await GetEnergyProfileAsync(accountId);
            //Console.WriteLine("value: " + energyProfile.ConfigurationValue);

            if (energyProfile == null)
            {
                return -1;
            }
            int newConfigurationValue = calculateNewConfigValue(efficiencyIndex, energyProfile.ConfigurationValue, configurationValue, configValues);

            Console.WriteLine(newConfigurationValue);
            return newConfigurationValue;

            //return configurationValue;
        }

        // Simulate implementation of function from EnergyEfficiency Analytics
        private double GetDeviceEnergyEfficiency(Guid deviceId)
        {
            Random random = new Random();
            double randomNumber = random.NextDouble() * 100.0;
            return randomNumber;
        }

        // Simulate getting List<Int>ConfigValueRange 
        // in this case an aircon range of degrees
        private List<int> ConfigValueRange(List<string> configInfo)
        {
            var range = new List<int>();
            if (configInfo[2] == "positive")
            {
                for (int i = int.Parse(configInfo[0]); i <= int.Parse(configInfo[1]); i++)
                {
                    range.Add(i);
                }
            }
            else if (configInfo[2] == "negative")
            {
                for (int i = int.Parse(configInfo[1]); i >= int.Parse(configInfo[0]); i--)
                {
                    range.Add(i);
                }
            }

            return range;
        }

        private int calculateNewConfigValue(double receivedEnergyEff, int energyProfileValue, int configurationValue, List<int> configurationValueRange)
        {
            double reductionMultiplier = 0.0;

            switch (energyProfileValue)
            {
                case 0:
                    reductionMultiplier = 0.0;
                    break;
                case 1:
                    reductionMultiplier = 0.5;
                    break;
                case 2:
                    reductionMultiplier = 1.0;
                    break;
                default:
                    break;
            }

            int currentIndex = configurationValueRange.IndexOf(configurationValue);
            // formula: shifts by 1/(1 + recommended adjustments * energyprofile aggressiveness)
            // magnitude 0 to 1, 0 is not changing anything, 1 is adjusting across whole range
            // max shift by magnitude of 1 (received 100, aggressiveness 100), min shift by 0
            int newIndex = currentIndex - (int)((configurationValueRange.Count - 1) * receivedEnergyEff / 100 * reductionMultiplier);

            // clamping
            if (newIndex < 0)
                newIndex = 0;

            return configurationValueRange[newIndex];
        }
    }
}
