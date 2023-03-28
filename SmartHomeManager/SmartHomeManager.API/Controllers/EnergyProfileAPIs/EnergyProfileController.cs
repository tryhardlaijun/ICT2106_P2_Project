using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.API;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Services;
using SmartHomeManager.Domain.EnergyProfileDomain.DTOs.Requests;
using SmartHomeManager.Domain.EnergyProfileDomain.Entities;
using SmartHomeManager.Domain.EnergyProfileDomain.Interfaces;
using SmartHomeManager.Domain.EnergyProfileDomain.Services;

namespace SmartHomeManager.API.Controllers.EnergyProfileAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyProfileController : ControllerBase
    {
        private readonly EnergyProfileServices _energyProfileService;

        public EnergyProfileController(IEnergyProfileRepository energyProfileRepo)
        {
            _energyProfileService = new(energyProfileRepo);
        }

/*        // GET: api/EnergyProfile
        [HttpGet("GetAllEnergyProfile")]
        public async Task<IEnumerable<EnergyProfile>> GetEnergyProfiles()
        {
            return await _energyProfileService.GetAllEnergyProfilesAsync();
        }*/

        [HttpGet("GetEnergyProfile")]
        public async Task<EnergyProfile?> GetEnergyProfile(Guid accountId)
        {
            return await _energyProfileService.GetEnergyProfileAsync(accountId);
        }

        [HttpPut("PutEnergyProfile/{accountId}")]
        public async Task<bool> PutEnergyProfile(Guid accountId, PutEnergyProfileWebRequest energyProfileWebRequest)
        {
            var response = await _energyProfileService.GetEnergyProfileAsync(accountId);
            Console.WriteLine(response);
            if (response == null) return false;
            
            return await _energyProfileService.PutEnergyProfileConfigValueAsync(accountId, energyProfileWebRequest.ConfigValue);
        }

    }
}
