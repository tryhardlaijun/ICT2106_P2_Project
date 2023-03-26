using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Services;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHomeManager.API.Controllers.RulesAPIs;

[Route("api/[controller]")]
[ApiController]
public class RulesController : ControllerBase
{
    private readonly RuleServices _registerRuleService;
    //private readonly GetRulesServices _getRulesServices;
      
    public RulesController(IGenericRepository<Rule> ruleRepository, IInformDirectorServices informDirectorServices,IGetRulesRepository getRulesRepository)
    {
        _registerRuleService = new(ruleRepository, getRulesRepository, informDirectorServices);
        //_getRulesServices = new(getRulesRepository);
    }

    // GET: api/Rules/GetAllRules
    [HttpGet("GetAllRules")]
    public async Task<ActionResult<IEnumerable<RuleRequest>>> GetAllRules()
    {
        var rules = await _registerRuleService.GetAllRulesAsync();
        if (rules != null)
        {
            var resp = rules.Select(rule => new RuleRequest
            {
                RuleId = rule.RuleId,
                ScenarioId = rule.ScenarioId,
                ConfigurationKey = rule.ConfigurationKey,
                ConfigurationValue = rule.ConfigurationValue,
                ActionTrigger = rule.ActionTrigger,
                RuleName = rule.RuleName,
                StartTime = (rule.StartTime != null) ? Convert.ToDateTime(rule.StartTime) : null,
                EndTime = (rule.EndTime != null) ? Convert.ToDateTime(rule.EndTime) : null,
                DeviceId = rule.DeviceId,
                APIKey = rule.APIKey,
                ApiValue = rule.ApiValue,
            }).ToList();
            return StatusCode(200,resp);
        }
        return StatusCode(404, "rule not exist");
    }

    // GET api/Rules/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Rule>> GetRule(Guid id)
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
    public async Task<ActionResult> CreateRule([FromBody] RuleRequest ruleRequest)
    {
        var rule = new Rule
        {
            RuleId = ruleRequest.RuleId,
            ScenarioId = ruleRequest.ScenarioId,
            ConfigurationKey = ruleRequest.ConfigurationKey,
            ConfigurationValue = ruleRequest.ConfigurationValue,
            ActionTrigger = ruleRequest.ActionTrigger,
            RuleName = ruleRequest.RuleName,
            StartTime = (ruleRequest.StartTime != null)?Convert.ToDateTime(ruleRequest.StartTime): null,
            EndTime = (ruleRequest.EndTime != null) ? Convert.ToDateTime(ruleRequest.EndTime) : null,
            DeviceId = ruleRequest.DeviceId,
            APIKey = ruleRequest.APIKey,
            ApiValue = ruleRequest.ApiValue,
        };
        if(await _registerRuleService.CreateRuleAsync(rule))
            return StatusCode(200, ruleRequest);
        return StatusCode(500, ruleRequest);
    }

    // PUT api/Rules/5
    [HttpPut("EditRule")]
    public async Task<ActionResult> EditRule(RuleRequest ruleRequest)
    {
        var rule = new Rule
        {
            RuleId = ruleRequest.RuleId,
            ScenarioId = ruleRequest.ScenarioId,
            ConfigurationKey = ruleRequest.ConfigurationKey,
            ConfigurationValue = ruleRequest.ConfigurationValue,
            ActionTrigger = ruleRequest.ActionTrigger,
            RuleName = ruleRequest.RuleName,
            StartTime = (ruleRequest.StartTime != null) ? Convert.ToDateTime(ruleRequest.StartTime) : null,
            EndTime = (ruleRequest.EndTime != null) ? Convert.ToDateTime(ruleRequest.EndTime) : null,
            DeviceId = ruleRequest.DeviceId,
            APIKey = ruleRequest.APIKey,
            ApiValue = ruleRequest.ApiValue,
        };
        if(await _registerRuleService.EditRuleAsync(rule))
            return StatusCode(200, rule);
        return StatusCode(500, rule);
    }

    // DELETE api/Rules/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRule(Guid id)
    {
        var rule = await _registerRuleService.GetRuleByIdAsync(id);
        if (rule != null)
        {
            await _registerRuleService.DeleteRuleByIdAsync(id);
            return StatusCode(200, rule);
        }
        return StatusCode(404, "rule not exist");
    }

    // To remove (Provided interface)
    [HttpGet("rulesByScenarioId/{id}")]
    public async Task<ActionResult<IEnumerable<RuleRequest?>>> GetRulesByScenarioId(Guid id)
    {
        var rules = await _registerRuleService.GetAllRulesByScenarioIdAsync(id);
        if (rules != null)
        {
            var resp = rules.Select(rule => new RuleRequest
            {
                RuleId = rule.RuleId,
                ScenarioId = rule.ScenarioId,
                ConfigurationKey = rule.ConfigurationKey,
                ConfigurationValue = rule.ConfigurationValue,
                ActionTrigger = rule.ActionTrigger,
                RuleName = rule.RuleName,
                StartTime = (rule.StartTime != null) ? Convert.ToDateTime(rule.StartTime) : null,
                EndTime = (rule.EndTime != null) ? Convert.ToDateTime(rule.EndTime) : null,
                DeviceId = rule.DeviceId,
                APIKey = rule.APIKey,
                ApiValue = rule.ApiValue,
            }).ToList();
            return StatusCode(200,resp);
        }
        return StatusCode(404, "rule not exist");
    }


    [HttpGet("schedulesByScenarioId/{id}")]
    public async Task<ActionResult<IEnumerable<RuleRequest?>>> GetScheduleByScenarioId(Guid id)
    {
        var rules = await _registerRuleService.GetSchedulesByScenarioIdAsync(id);
        if (rules != null)
        {
            var resp = rules.Select(rule => new RuleRequest
            {
                RuleId = rule.RuleId,
                ScenarioId = rule.ScenarioId,
                ConfigurationKey = rule.ConfigurationKey,
                ConfigurationValue = rule.ConfigurationValue,
                ActionTrigger = rule.ActionTrigger,
                RuleName = rule.RuleName,
                StartTime = (rule.StartTime != null) ? Convert.ToDateTime(rule.StartTime) : null,
                EndTime = (rule.EndTime != null) ? Convert.ToDateTime(rule.EndTime) : null,
                DeviceId = rule.DeviceId,
                APIKey = rule.APIKey,
                ApiValue = rule.ApiValue,
            }).ToList();
            return StatusCode(200, resp);
        }
        return StatusCode(404, "Schedule not exist");
    }

    [HttpGet("eventsByScenarioId/{id}")]
    public async Task<ActionResult<IEnumerable<RuleRequest?>>> GetEventByScenarioId(Guid id)
    {
        var rules = await _registerRuleService.GetEventsByScenarioIdAsync(id);
        if (rules != null)
        {
            var resp = rules.Select(rule => new RuleRequest
            {
                RuleId = rule.RuleId,
                ScenarioId = rule.ScenarioId,
                ConfigurationKey = rule.ConfigurationKey,
                ConfigurationValue = rule.ConfigurationValue,
                ActionTrigger = rule.ActionTrigger,
                RuleName = rule.RuleName,
                StartTime = (rule.StartTime != null) ? Convert.ToDateTime(rule.StartTime) : null,
                EndTime = (rule.EndTime != null) ? Convert.ToDateTime(rule.EndTime) : null,
                DeviceId = rule.DeviceId,
                APIKey = rule.APIKey,
                ApiValue = rule.ApiValue,
            }).ToList();
            return StatusCode(200, resp);
        }
        return StatusCode(404, "Events not exist");
    }

    [HttpGet("apisByScenarioId/{id}")]
    public async Task<ActionResult<IEnumerable<RuleRequest?>>> GetApiByScenarioId(Guid id)
    {
        var rules = await _registerRuleService.GetApisByScenarioIdAsync(id);
        if (rules != null)
        {
            var resp = rules.Select(rule => new RuleRequest
            {
                RuleId = rule.RuleId,
                ScenarioId = rule.ScenarioId,
                ConfigurationKey = rule.ConfigurationKey,
                ConfigurationValue = rule.ConfigurationValue,
                ActionTrigger = rule.ActionTrigger,
                RuleName = rule.RuleName,
                StartTime = (rule.StartTime != null) ? Convert.ToDateTime(rule.StartTime) : null,
                EndTime = (rule.EndTime != null) ? Convert.ToDateTime(rule.EndTime) : null,
                DeviceId = rule.DeviceId,
                APIKey = rule.APIKey,
                ApiValue = rule.ApiValue,
            }).ToList();
            return StatusCode(200, resp);
        }
        return StatusCode(404, "Apis not exist");
    }

    // To do : loadRulesBackup(ProfileId, IEnumerable<Rules>)

    [HttpGet("DownloadRules")]
    public async Task<IActionResult> DownloadRules(Guid ScenarioId)
    {
        var ruleBytes = await _registerRuleService.DownloadRules(ScenarioId);
        return File(ruleBytes, "application/json", "rules.json");
    }

    [HttpPost("UploadRules")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var result = await _registerRuleService.UploadRules(file);
        if (result){
            return StatusCode(200, file);
        } else{
            return StatusCode(500);
        }
    }
    [HttpPost("CheckIfClash")]
    public async Task<IActionResult> CheckIfClash(RuleRequest ruleRequest)
    {
        var result = await _registerRuleService.RuleClashesAsync(ruleRequest);
        if (result == null)
        {
            return StatusCode(200, ruleRequest);
        }
        else
        {
            return StatusCode(409, result);
        }
    }

    [HttpPost("OverWrite")]
    public async Task<IActionResult> Overwrite(RuleRequest ruleRequest)
    {
        var clashingRuleRequest = await _registerRuleService.RuleClashesAsync(ruleRequest);
        if (clashingRuleRequest != null)
        {
            await _registerRuleService.DeleteRuleByIdAsync(clashingRuleRequest.RuleId);
            return StatusCode(200, ruleRequest);
        }
        else
        {
            return StatusCode(409, "No clashing rule found");

        }
    }
}