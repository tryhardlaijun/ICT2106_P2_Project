using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.DirectorDomain.Services;
using SmartHomeManager.Domain.EnergyProfileDomain.DTOs.Requests;
using SmartHomeManager.Domain.HomeSecurityDomain.DTOs.Requests;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using SmartHomeManager.Domain.HomeSecurityDomain.Interfaces;
using SmartHomeManager.Domain.HomeSecurityDomain.Services;

namespace SmartHomeManager.API.Controllers.HomeSecurityAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeSecurityController : ControllerBase
    {
        private readonly HomeSecurityServices _homeSecurityService;

        public HomeSecurityController(IHomeSecurityRepository<HomeSecurity> homeSecurityRepo, IHomeSecuritySettingRepository<HomeSecuritySetting> homeSecuritySettingRepo, IHomeSecurityDeviceDefinitionRepository<HomeSecurityDeviceDefinition> homeSecurityDeviceDefinitionRepo, IDirectorServices directorServices)
        {
            _homeSecurityService = new(homeSecurityRepo, homeSecuritySettingRepo, homeSecurityDeviceDefinitionRepo, directorServices);
        }

        // GET: api/HomeSecurity
        [HttpGet("GetSecurityMode")]
        public async Task<bool> GetHomeSecurity(Guid accountId)
        {
            var homeSecurity = await _homeSecurityService.getSecurityMode(accountId);
            return homeSecurity.SecurityModeState;
        }

        // GET: api/HomeSecurity
        [HttpGet("IsAccountAlerted")]
        public bool isAccountAlerted(Guid accountId)
        {
            return _homeSecurityService.isAccountAlerted(accountId);
        }

        // GET: api/HomeSecurity
        [HttpGet("IsAccountLockedDown")]
        public bool isAccountLockedDown(Guid accountId)
        {
            return _homeSecurityService.isAccountLockedDown(accountId);
        }

        // PUT: api/HomeSecurity
        [HttpPut("PutSecurityMode/{accountId}")]
        public async Task<bool> PutSecurityMode(Guid accountId, PutSecurityModeRequest securityModeWebRequest)
        {
            return await _homeSecurityService.setSecurityMode(accountId, securityModeWebRequest.SecurityMode);
        }

        // PUT: api/HomeSecurity
        [HttpPut("PutHomeSecuritySettings")]
        public async Task<bool> PutHomeSecuritySettings(Guid homeSecurityId, PutSecuritySettingsEnabledRequest securitySettingsEnabledWebRequest)
        {
            return await _homeSecurityService.setHomeSecuritySettings(homeSecurityId, securitySettingsEnabledWebRequest.DeviceGroup, securitySettingsEnabledWebRequest.Enabled);
        }
    }
}
