using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Services;
using SmartHomeManager.Domain.Common;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.DeviceDomain.Entities;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHomeManager.API.Controllers.RulesAPIs;

[Route("api/[controller]")]
[ApiController]
public class RulesController : ControllerBase
{

    private readonly RuleServices _registerRuleService;

    public RulesController(IGenericRepository<Rule> ruleRepository)
    {
        _registerRuleService = new(ruleRepository);
    }
    // GET: api/Rules/GetAllRules
    [HttpGet("GetAllRules")]
    public async Task<ActionResult> GetAllRules()
    {
        IEnumerable<Rule> rules = (await _registerRuleService.GetAllRulesAsync()).ToList();
        return StatusCode(200, rules);
    }

    // GET api/Rules/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Rule>> GetRule([FromBody] Guid id)
    {
        var rule = await _registerRuleService.GetRuleByIdAsync(id);
        if(rule != null)
        {
            return StatusCode(200, rule);
        }
        return StatusCode(404, "rule not exist");
    }

    // POST api/Rules
    [HttpPost("CreateRule")]
    public async Task CreateRule([FromBody] Rule rule)
    {
        await _registerRuleService.CreateRuleAsync(rule);
    }

    // PUT api/Rules/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {

    }

    // DELETE api/Rules/1
    [HttpDelete("{id}")]
    public async Task DeleteRule([FromBody]Guid id)
    {
        await _registerRuleService.DeleteRuleByIdAsync(id);
    }
}

