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

namespace SmartHomeManager.API.Controllers.HistoryAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly HistoryServices _ruleHistoryService;

        public HistoryController(IGenericRepository<History> historyRepo)
        {
            _ruleHistoryService = new(historyRepo);
        }

        // GET: api/History
        [HttpGet("GetAllHistory")]
        public async Task<IEnumerable<History>> GetRuleHistories()
        {
            /*if (_ruleHistoryService.RuleHistors == null)
            {
                return NotFound();
            }*/
            return await _ruleHistoryService.GetAllRulesAsync();
        }
        
    }
}
