﻿using Microsoft.AspNetCore.Mvc;
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
    // Factory
    // Factory choose which ruleService to use.
    // Parse in something from frontend 
    private readonly GetRulesServices _getRulesServices;
      

    public RulesController(IGenericRepository<Rule> ruleRepository, IInformDirectorServices informDirectorServices,IGetRulesRepository getRulesRepository)
    {
        _registerRuleService = new(ruleRepository, informDirectorServices);
        _getRulesServices = new(getRulesRepository);
    }

    // GET: api/Rules/GetAllRules
    [HttpGet("GetAllRules")]
    public async Task<IEnumerable<RuleRequest>> GetAllRules()
    {
        var rules = await _getRulesServices.GetAllRulesAsync();
        var resp = rules.Select(rule => new RuleRequest
        {
            RuleId = rule.RuleId,
            ScenarioId = rule.ScenarioId,
            ConfigurationKey = rule.ConfigurationKey,
            ConfigurationValue = rule.ConfigurationValue,
            ActionTrigger = rule.ActionTrigger,
            RuleName = rule.RuleName,
            StartTime = Convert.ToDateTime(rule.StartTime),
            EndTime = Convert.ToDateTime(rule.EndTime),
            DeviceId = rule.DeviceId,
            APIKey = rule.APIKey,
            ApiValue = rule.ApiValue,
        }).ToList();
        return resp;
    }

    // GET api/Rules/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Rule>> GetRule(Guid id)
    {
        var rule = await _getRulesServices.GetRuleByIdAsync(id);
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
        if (await _registerRuleService.RuleClashesAsync(ruleRequest))
        {
            return StatusCode(409, "Rule clashes with existing rule.");
        }
        var rule = new Rule
        {
            RuleId = ruleRequest.RuleId,
            ScenarioId = ruleRequest.ScenarioId,
            ConfigurationKey = ruleRequest.ConfigurationKey,
            ConfigurationValue = ruleRequest.ConfigurationValue,
            ActionTrigger = ruleRequest.ActionTrigger,
            RuleName = ruleRequest.RuleName,
            StartTime = Convert.ToDateTime(ruleRequest.StartTime),
            EndTime = Convert.ToDateTime(ruleRequest.EndTime),
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
        if (await _registerRuleService.RuleClashesAsync(ruleRequest))
        {
            return StatusCode(409, "Rule clashes with existing rule.");
        }
        var rule = new Rule
        {
            RuleId = ruleRequest.RuleId,
            ScenarioId = ruleRequest.ScenarioId,
            ConfigurationKey = ruleRequest.ConfigurationKey,
            ConfigurationValue = ruleRequest.ConfigurationValue,
            ActionTrigger = ruleRequest.ActionTrigger,
            RuleName = ruleRequest.RuleName,
            StartTime = Convert.ToDateTime(ruleRequest.StartTime),
            EndTime = Convert.ToDateTime(ruleRequest.EndTime),
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
    public async Task<IEnumerable<RuleRequest?>> GetRulesByScenarioId(Guid id)
    {
        var rules = await _getRulesServices.GetAllRulesByScenarioIdAsync(id);
        var resp = rules.Select(rule => new RuleRequest
        {
            RuleId = rule.RuleId,
            ScenarioId = rule.ScenarioId,
            ConfigurationKey = rule.ConfigurationKey,
            ConfigurationValue = rule.ConfigurationValue,
            ActionTrigger = rule.ActionTrigger,
            RuleName = rule.RuleName,
            StartTime = Convert.ToDateTime(rule.StartTime),
            EndTime = Convert.ToDateTime(rule.EndTime),
            DeviceId = rule.DeviceId,
            APIKey = rule.APIKey,
            ApiValue = rule.ApiValue,
        }).ToList();
        return resp;
    }

    // To do : loadRulesBackup(ProfileId, IEnumerable<Rules>)

    [HttpGet("DownloadRules")]
    public async Task<IActionResult> DownloadRules(Guid ScenarioId)
    {
        var ruleBytes = await _getRulesServices.DownloadRules(ScenarioId);
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
}