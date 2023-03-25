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
            "camera", "microphone"
        };

        private IHomeSecurityRepository<HomeSecurity> _homeSecurityRepository;
        private IHomeSecuritySettingRepository<HomeSecuritySetting> _homeSecuritySettingRepository;
        private IHomeSecurityDeviceDefinitionRepository<HomeSecurityDeviceDefinition> _homeSecurityDeviceDefinitionRepository;

        private readonly IDirectorServices _directorInterface;
        public HomeSecurityServices(IHomeSecurityRepository<HomeSecurity> homeSecurityRepo, IHomeSecuritySettingRepository<HomeSecuritySetting> homeSecuritySettingRepo, IHomeSecurityDeviceDefinitionRepository<HomeSecurityDeviceDefinition> homeSecurityDeviceDefinitionRepo, IDirectorServices directorServices)
        {
            alertedAccounts.Add(new Guid("11111111-1111-1111-1111-111111111111"));
            _homeSecurityRepository = homeSecurityRepo;
            _homeSecuritySettingRepository = homeSecuritySettingRepo;
            _homeSecurityDeviceDefinitionRepository = homeSecurityDeviceDefinitionRepo;
            _directorInterface = directorServices;
            initialiseCompatibleDevicesList();
        }

        /*
         * Helper function to initialise homeSecurityDeviceDefinitions.
         */
        private async void initialiseCompatibleDevicesList()
        {
            homeSecurityDeviceDefinitions = await _homeSecurityDeviceDefinitionRepository.GetAllAsync();
        }

        /*
         * Helper function to translate accountID to HomeSecurity Object.
         * If there isn't an associated HomeSecurity in the database, it calls for its creation.
         * Contains a mutex to ensure that multiple instances aren't called to be created.
         */
        private static Mutex mut = new Mutex();
        private async Task<HomeSecurity> getHomeSecurity(Guid accountID)
        {
            // getter of homesecurity : securitymodestate
            // for front end & back end
            HomeSecurity? homeSecurityObj = await _homeSecurityRepository.GetByAccountIdAsync(accountID);
            if (homeSecurityObj != null)
            {
                return homeSecurityObj;
            }
            else
            {
                if (mut.WaitOne(0))
                {
                    await createHomeSecurityAndSettings(accountID);
                }

                homeSecurityObj = await getHomeSecurity(accountID);

                return homeSecurityObj;
            }
        }

        /*
         * Helper function to create HomeSecurity and HomeSecuritySettings Object.
         * If there isn't an associated HomeSecurity and its HomeSecuritySettings in the database, it calls for their creation.
         */
        public async Task<bool> createHomeSecurityAndSettings(Guid accountID)
        {
            HomeSecurity newHomeSecurityObj = new HomeSecurity();
            newHomeSecurityObj.HomeSecurityId = Guid.NewGuid();
            newHomeSecurityObj.AccountId = accountID;
            newHomeSecurityObj.SecurityModeState = false;
            await _homeSecurityRepository.AddAsync(newHomeSecurityObj);

            foreach (HomeSecurityDeviceDefinition homeSecurityDeviceDefinition in homeSecurityDeviceDefinitions!)
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

            return true;
        }

        /*
         * TOWRITE
         */
        public async void processEventAsync(Guid accountID, string deviceGroup, string configurationKey, int configurationValue)
        {
            // Checks if device is a detector
            if (!detectorDeviceGroups.Contains(deviceGroup))
                return;

            HomeSecurity homeSecurityObj = await getHomeSecurity(accountID);
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
         * TOWRITE
         */
        public bool isAccountAlerted(Guid accountID)
        {
            return alertedAccounts.Contains(accountID);
        }

        /*
         * TOWRITE
         */
        public bool isAccountLockedDown(Guid accountID)
        {
            return lockedDownAccounts.Contains(accountID);
        }

        /*
         * Called by Frontend to get HomeSecurity.SecurityModeState based on accountId.
         */
        public async Task<bool> getSecurityState(Guid accountID)
        {
            return (await getHomeSecurity(accountID)).SecurityModeState;
        }

        /*
         * Called by Frontend to set HomeSecurity.SecurityModeState based on accountId.
         */
        public async Task<bool> setSecurityMode(Guid accountID, bool securityModeState)
        {
            HomeSecurity homeSecurityObj = await getHomeSecurity(accountID);
            homeSecurityObj.SecurityModeState = securityModeState;

            return await _homeSecurityRepository.UpdateAsync(homeSecurityObj);
        }

        /*
         * Called by Frontend to get a list of HomeSecuritySettings based on accountId.
         */
        public async Task<IEnumerable<HomeSecuritySetting>> getHomeSecuritySettings(Guid accountID)
        {
            HomeSecurity homeSecurityObj = await getHomeSecurity(accountID);
            Guid homeSecurityID = homeSecurityObj.HomeSecurityId;

            return (await _homeSecuritySettingRepository.GetByHomeSecurityIdAsync(homeSecurityID))!;
        }

        /*
         * Called by Frontend to set a specific HomeSecuritySettings based on accountId.
         */
        public async Task<bool> setHomeSecuritySettings(Guid accountID, string deviceGroup, bool enabled)
        {
            // setter of homesecuritysettings
            IEnumerable<HomeSecuritySetting> homeSecuritySettingObjEnum = await getHomeSecuritySettings(accountID);
            foreach (HomeSecuritySetting homeSecuritySettingObj in homeSecuritySettingObjEnum)
            {
                if (homeSecuritySettingObj.DeviceGroup == deviceGroup)
                {
                    homeSecuritySettingObj.Enabled = enabled;
                    return await _homeSecuritySettingRepository.UpdateAsync(homeSecuritySettingObj);
                }
            }

            return false;
        }

        /*
         * TOWRITE
         */
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
