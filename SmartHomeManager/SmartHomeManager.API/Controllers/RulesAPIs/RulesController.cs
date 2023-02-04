using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Services;
using SmartHomeManager.Domain.Common;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;


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
    public async Task<IEnumerable<Rule>> GetAllRules()
    {
        return await _registerRuleService.GetAllRulesAsync();
    }

    // GET api/Rules/1
    //[HttpGet("{id}")]
    //public async Task<ActionResult<Rule>> GetRule([FromBody] Guid id)
    //{
    //    var rule = await _registerRuleService.GetRuleByIdAsync(id);
    //    if(rule != null)
    //    {
    //        return StatusCode(200, rule);
    //    }
    //    return StatusCode(404, "rule not exist");
    //}

    // POST api/Rules
    [HttpPost("CreateRule")]
    public async Task<ActionResult> CreateRule(Rule rule)
    {
        await _registerRuleService.CreateRuleAsync(rule);
        return StatusCode(200, rule);
    }

    //// PUT api/Rules/5
    //[HttpPut("{id}")]
    //public void Put(int id, [FromBody] string value)
    //{

    //}

    //// DELETE api/Rules/1
    //[HttpDelete("{id}")]
    //public async Task DeleteRule([FromBody]Guid id)
    //{
    //    await _registerRuleService.DeleteRuleByIdAsync(id);
    //}
}

