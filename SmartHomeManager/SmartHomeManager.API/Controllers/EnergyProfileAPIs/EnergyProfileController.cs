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
using SmartHomeManager.Domain.EnergyProfileDomain.Entities;
using SmartHomeManager.Domain.EnergyProfileDomain.Services;

namespace SmartHomeManager.API.Controllers.EnergyProfileAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyProfileController : ControllerBase
    {
        private readonly EnergyProfileServices _energyProfileService;

        public EnergyProfileController(IGenericRepository<EnergyProfile> energyProfileRepo)
        {
            _energyProfileService = new(energyProfileRepo);
        }

        // GET: api/EnergyProfile
        [HttpGet("GetAllEnergyProfile")]
        public async Task<IEnumerable<EnergyProfile>> GetEnergyProfiles()
        {
            /*if (_ruleHistoryService.RuleHistors == null)
            {
                return NotFound();
            }*/
            return await _energyProfileService.GetAllRulesAsync();
        }

    }
}
