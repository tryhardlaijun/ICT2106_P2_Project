using Microsoft.Extensions.DependencyInjection;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using SmartHomeManager.Domain.HomeSecurityDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.HomeSecurityDomain.Services
{
    public class HomeSecurityServices : IHomeSecurityServices
    {
        private readonly IServiceProvider _serviceProvider;
        private List<Guid>? alertedAccounts;
        private List<Guid>? lockedDownAccounts;

        private IGenericRepository<HomeSecuritySetting> _homeSecuritySettingRepository;
        private IHomeSecurityDeviceDefinitionRepository<HomeSecurityDeviceDefinition> _homeSecurityDeviceDefinitionRepository;

        public HomeSecurityServices(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var scope = _serviceProvider.CreateScope();
            _homeSecuritySettingRepository = scope.ServiceProvider.GetRequiredService<IGenericRepository<HomeSecuritySetting>>();
            _homeSecurityDeviceDefinitionRepository = scope.ServiceProvider.GetRequiredService<IHomeSecurityDeviceDefinitionRepository<HomeSecurityDeviceDefinition>>();
        }

        public List<string> getHomeSecurityCompatibleDevices()
        {
            // gives all devices director should ask us to process
            throw new NotImplementedException();
        }

        public void processEvent(Guid accountID)
        {
            // if accountid securitymodestate true
            // update list of alertedaccounts
            throw new NotImplementedException();
        }

        public bool isAccountAlerted(Guid accountID)
        {
            // polled by frontend
            // check list of alertedaccounts if contain accid
            throw new NotImplementedException();
        }

        void getSecurityMode(Guid accountID)
        {
            // getter of homesecurity : securitymodestate
            // for front end
            throw new NotImplementedException();
        }

        void setSecurityMode(Guid accountID, bool state)
        {
            // setter of homesecurity : securitymodestate
            throw new NotImplementedException();
        }

        void updateHomeSecuritySettings(HomeSecurity homeSecurity)
        {
            // setter of homesecuritysettings
            throw new NotImplementedException();
        }

        void setLockdownState(Guid accountID, bool state)
        {
            // called by front end
            // after user selects state when lockdown prompt
            // or when turning off lockdown
            // calls director's executeSecurityProtocol(boolean, HomeSecurityDeviceDefinitions)
            // setter of list of lockeddownaccounts
        }
    }
}
