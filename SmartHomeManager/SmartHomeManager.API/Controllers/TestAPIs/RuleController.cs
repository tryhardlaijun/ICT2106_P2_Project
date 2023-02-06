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
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.API.Controllers.TestAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly RuleServices _ruleService;

        public RuleController(IGenericRepository<Rule> ruleRepo)
        {
            _ruleService = new(ruleRepo);
        }

        // GET: api/History
        [HttpGet("GetAllRule")]
        public async Task<IEnumerable<Rule>> GetRuleHistories()
        {
            return await _ruleService.GetAllRulesAsync();
        }
        
    }
}
