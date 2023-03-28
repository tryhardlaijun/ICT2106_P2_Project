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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Services
{
    public class HomeSecurityServices : IHomeSecurityServices
    {
        private static List<Guid> alertedAccounts = new List<Guid>();
        private static List<Guid> lockedDownAccounts = new List<Guid>();
        private static List<KeyValuePair<Guid, string>> policeContacted = new List<KeyValuePair<Guid, string>>();

        private static List<KeyValuePair<Guid, string>> triggeredDeviceLog = new List<KeyValuePair<Guid, string>>();
        private IEnumerable<HomeSecurityDeviceDefinition>? homeSecurityDeviceDefinitions;
        List<string> detectorDeviceGroups = new List<string>() {
            "Camera", "Microphone"
        };

        private IHomeSecurityRepository _homeSecurityRepository;
        private IHomeSecuritySettingRepository _homeSecuritySettingRepository;
        private IHomeSecurityDeviceDefinitionRepository _homeSecurityDeviceDefinitionRepository;

        private readonly IDirectorServices _directorInterface;
        public HomeSecurityServices(IHomeSecurityRepository homeSecurityRepo, IHomeSecuritySettingRepository homeSecuritySettingRepo, IHomeSecurityDeviceDefinitionRepository homeSecurityDeviceDefinitionRepo, IDirectorServices directorServices)
        {
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
         * Function is called whenever a device changes state.
         * Checks if device in question is a notable one (in detectorDeviceGroups), then checks that 
         * SecurityModeState is true for accountID of device, then checks if 
         * device trigger matches detection signature.
         * If all checks pass, accountID is added to alertedAccounts.
         */
        public async void processEventAsync(Guid accountID, string deviceGroup, string configurationKey, int configurationValue)
        {
            string triggeredDevice = "";
            string correspondingKey = "";
            string finalString = "";
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

                    triggeredDevice = deviceGroup.Substring(0, 1).ToUpper() + deviceGroup.Substring(1);
                    correspondingKey = configurationKey.Substring(0, 1).ToUpper() + configurationKey.Substring(1);

                    finalString = triggeredDevice + " has detected " + correspondingKey + "!";
                    triggeredDeviceLog.Add(new KeyValuePair<Guid, string>(accountID, finalString));
                    foreach (KeyValuePair<Guid, string> devicelog in triggeredDeviceLog)
                    {
                        Console.WriteLine(devicelog.Value);
                    }
                    break;
                }
            }
        }

        /*
         * Called by Frontend to get a boolean based on if accountId is found in alertedAccounts.
         */
        public bool isAccountAlerted(Guid accountID)
        {
            return alertedAccounts.Contains(accountID);
        }

        /*
         * Called by Frontend to get a boolean based on if accountId is found in lockedDownAccounts.
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
        public List<string> getTriggeredDeviceLogs(Guid accountID)
        {
            List<string> deviceLogs = new List<string>();
            foreach (KeyValuePair<Guid, string> triggeredlog in triggeredDeviceLog)
            {
                if (triggeredlog.Key == accountID)
                {
                    deviceLogs.Add(triggeredlog.Value);
                }
            }
            return deviceLogs;
        }

        /*
         * Called by Frontend to choose lockdown state.
         * True: Adds accountID to lockedDownAccounts.
         * False: Removes accountID from alertedAccounts & lockedDownAccounts.
         * Finally: Calls director function to trigger all activated devices in accountID's HomeSecuritySettings.
         */
        public async void setLockdownState(Guid accountID, bool setTo)
        {
            if (setTo)
            {
                lockedDownAccounts.Add(accountID);
            }
            else
            {
                alertedAccounts.Remove(accountID);
                lockedDownAccounts.Remove(accountID);
            }

            //for (int i = 0; i < alertedAccounts.Count; i++)
            //{
            //    Console.WriteLine("alert: " + alertedAccounts[i]);
            //}
            //for (int i = 0; i < lockedDownAccounts.Count; i++)
            //{
            //    Console.WriteLine("lock: " + lockedDownAccounts[i]);
            //}

            IEnumerable<HomeSecuritySetting> enabledSettings = await getHomeSecuritySettings(accountID);
            foreach (HomeSecuritySetting setting in enabledSettings)
            {
                if(setting.Enabled)
                {
                    //Console.WriteLine("Inside Setting Lockdown State " + setting.HomeSecurityDeviceDefinition.DeviceGroup);
                    _directorInterface.executeSecurityProtocol(accountID, setTo, setting.HomeSecurityDeviceDefinition);
                }
            }
            
        }

        /*
         * Called by Frontend to get a boolean based on if accountId is found in alertedAccounts.
         */
        public void setPoliceContacted(Guid accountId)
        {
            policeContacted.Add(new KeyValuePair<Guid, string>(accountId, "Police has been contacted!"));
        }

        /*
         * Called by Frontend to get a boolean based on if accountId is found in alertedAccounts.
         */
        public IEnumerable<string> getPoliceContacted(Guid accountId)
        {
            List<string> policeContactedLogs = new List<string>();
            foreach (KeyValuePair<Guid, string> contactedLog in policeContacted)
            {
                if (contactedLog.Key == accountId)
                {
                    policeContactedLogs.Add(contactedLog.Value);
                    Console.WriteLine(contactedLog.Value);
                }
            }
            return policeContactedLogs;
        }
    }
}
