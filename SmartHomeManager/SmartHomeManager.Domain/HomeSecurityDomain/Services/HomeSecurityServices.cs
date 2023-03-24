using Microsoft.Extensions.DependencyInjection;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.DirectorDomain.Services;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using SmartHomeManager.Domain.HomeSecurityDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Services
{
    public class HomeSecurityServices : IHomeSecurityServices
    {
        private List<Guid> alertedAccounts = new List<Guid>();
        private List<Guid> lockedDownAccounts = new List<Guid>();
        private IEnumerable<HomeSecurityDeviceDefinition>? homeSecurityDeviceDefinitions;
        List<string> detectorDeviceGroups = new List<string>() { 
            "Camera", "Microphone" 
        };

        private IHomeSecurityRepository<HomeSecurity> _homeSecurityRepository;
        private IHomeSecuritySettingRepository<HomeSecuritySetting> _homeSecuritySettingRepository;
        private IHomeSecurityDeviceDefinitionRepository<HomeSecurityDeviceDefinition> _homeSecurityDeviceDefinitionRepository;

        private readonly IDirectorServices _directorInterface;
        public HomeSecurityServices(IHomeSecurityRepository<HomeSecurity> homeSecurityRepo, IHomeSecuritySettingRepository<HomeSecuritySetting> homeSecuritySettingRepo, IHomeSecurityDeviceDefinitionRepository<HomeSecurityDeviceDefinition> homeSecurityDeviceDefinitionRepo, IDirectorServices directorServices)
        {
            _homeSecurityRepository = homeSecurityRepo;
            _homeSecuritySettingRepository = homeSecuritySettingRepo;
            _homeSecurityDeviceDefinitionRepository = homeSecurityDeviceDefinitionRepo;
            _directorInterface = directorServices;
            initialiseCompatibleDevicesList();
        }


        private async void initialiseCompatibleDevicesList()
        {
            homeSecurityDeviceDefinitions = await _homeSecurityDeviceDefinitionRepository.GetAllAsync();
        }

        // helper function to translate accountID to HomeSecurityID
        private async Task<HomeSecurity> getHomeSecurityId(Guid accountID)
        {
            return (await _homeSecurityRepository.GetByAccountIdAsync(accountID))!;
        }

        public async void processEventAsync(Guid accountID, string deviceGroup, string configurationKey, int configurationValue)
        {
            // Checks if device is a detector
            if (!detectorDeviceGroups.Contains(deviceGroup))
                return;

            HomeSecurity homeSecurityObj = await getSecurityMode(accountID);
            // Security Mode for this account is not enabled
            if (!homeSecurityObj.SecurityModeState)
                return;

            // Check if deviceGroup, configurationKey, configurationValue match a valid detectorDeviceGroup item
            foreach (HomeSecurityDeviceDefinition homeSecurityDeviceDefinition in homeSecurityDeviceDefinitions!)
            {
                if (homeSecurityDeviceDefinition.DeviceGroup == deviceGroup &&
                    homeSecurityDeviceDefinition.ConfigurationKey == configurationKey &&
                    homeSecurityDeviceDefinition.ConfigurationOnValue == configurationValue)
                {
                    alertedAccounts.Add(accountID);
                    break;
                }
            }
        }

        /*
         * polled by frontend
         */
        public bool isAccountAlerted(Guid accountID)
        {
            return alertedAccounts.Contains(accountID);
        }

        /*
         * polled by frontend
         */
        public bool isAccountLockedDown(Guid accountID)
        {
            return lockedDownAccounts.Contains(accountID);
        }

        public async Task<HomeSecurity> getSecurityMode(Guid accountID)
        {
            // getter of homesecurity : securitymodestate
            // for front end & back end
            HomeSecurity? homeSecurityObj = await getHomeSecurityId(accountID);

            if (homeSecurityObj != null)
            {
                return homeSecurityObj;
            }
            else
            {
                HomeSecurity newHomeSecurityObj = new HomeSecurity();
                newHomeSecurityObj.HomeSecurityId = Guid.NewGuid();
                newHomeSecurityObj.AccountId = accountID;
                newHomeSecurityObj.SecurityModeState = false;
                await _homeSecurityRepository.AddAsync(newHomeSecurityObj);

                foreach(HomeSecurityDeviceDefinition homeSecurityDeviceDefinition in homeSecurityDeviceDefinitions!)
                {
                    if (detectorDeviceGroups.Contains(homeSecurityDeviceDefinition.DeviceGroup))
                        continue;

                    HomeSecuritySetting newHomeSecuritySetting = new HomeSecuritySetting();
                    newHomeSecuritySetting.HomeSecuritySettingId = Guid.NewGuid();
                    newHomeSecuritySetting.HomeSecurityId = newHomeSecurityObj.HomeSecurityId;
                    newHomeSecuritySetting.DeviceGroup = homeSecurityDeviceDefinition.DeviceGroup;
                    newHomeSecuritySetting.Enabled = false;
                    await _homeSecuritySettingRepository.AddAsync(newHomeSecuritySetting);
                }

                return newHomeSecurityObj;
            }
        }

        // frontend calls
        public async Task<bool> setSecurityMode(Guid accountID, bool securityModeState)
        {
            HomeSecurity? homeSecurityObj = await getHomeSecurityId(accountID);
            if (homeSecurityObj != null)
            {
                homeSecurityObj.SecurityModeState = securityModeState;
                return await _homeSecurityRepository.UpdateAsync(homeSecurityObj);
            }
            return false;
        }

        // frontend calls
        public async Task<IEnumerable<HomeSecuritySetting>> getHomeSecuritySettings(Guid accountID)
        {
            HomeSecurity? homeSecurityObj = await getHomeSecurityId(accountID);
            if (homeSecurityObj != null)
            {
                Guid homeSecurityID = homeSecurityObj.HomeSecurityId;
                return (await _homeSecuritySettingRepository.GetByHomeSecurityIdAsync(homeSecurityID))!;
            }

            return Enumerable.Empty<HomeSecuritySetting>();
        }

        // frontend calls
        public async Task<bool> setHomeSecuritySettings(Guid homeSecurityId, string deviceGroup, bool enabled)
        {
            // setter of homesecuritysettings
            IEnumerable<HomeSecuritySetting> homeSecuritySettingObjEnum = await getHomeSecuritySettings(homeSecurityId);
            foreach (HomeSecuritySetting homeSecuritySettingObj in homeSecuritySettingObjEnum)
            {
                if (homeSecuritySettingObj.DeviceGroup == deviceGroup)
                {
                    if (homeSecuritySettingObj.Enabled != enabled)
                    {
                        homeSecuritySettingObj.Enabled = enabled;
                        return await _homeSecuritySettingRepository.UpdateAsync(homeSecuritySettingObj);
                    }
                    break;
                }
            }
            return false;
        }

        // frontend calls
        public void setLockdownState(Guid accountID, bool securityModeState)
        {
            // called by front end
            // after user selects state when lockdown prompt
            // or when turning off lockdown
            // calls director's executeSecurityProtocol(boolean, HomeSecurityDeviceDefinitions)


            if (securityModeState)
            {
                lockedDownAccounts.Add(accountID);
            }
            else
            {
                alertedAccounts.Remove(accountID);
                lockedDownAccounts.Remove(accountID);
            }


            // _directorInterface.executeSecurityProtocol(accountID, securityModeState, );
        }
    }
}
