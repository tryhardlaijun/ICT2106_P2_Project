using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.Common;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHomeManager.API.Controllers.RulesAPIs;

[Route("api/[controller]")]
[ApiController]
public class RulesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RulesController(ApplicationDbContext context)
    {
        _context = context;
    }
    // GET: api/Rules/GetAllRules
    [HttpGet("GetAllRules")]
    public async Task<ActionResult<IEnumerable<Rule>>> GetAllRules()
    {
        if(_context.Rules == null)
        {
            return NotFound();
        }
        var result = await _context.Rules.ToListAsync();

        return Ok(result);
    }

    // GET api/Rules/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Rule>> GetRule([FromBody] Guid id)
    {
        if(_context.Rules == null)
        {
            return NotFound();
        }
        var rule = await _context.Rules.FindAsync(id);
        if(rule == null)
        {
            return NotFound();
        }
        return rule;
    }

    // POST api/Rules
    [HttpPost("CreateRule")]
    public async Task<ActionResult<Rule>> CreateRule([FromBody] Rule rule)
    {
        if(_context.Rooms == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Rules'  is null.");
        }
        _context.Rules.Add(rule);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRule", new { id = rule.RuleId }, rule);
    }

    // PUT api/Rules/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {

    }

    // DELETE api/Rules/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRules([FromBody]Guid id)
    { 
        if(_context.Rules == null)
        {
            return NotFound();
        }
        var rule = await _context.Rules.FindAsync(id);
        if(rule == null)
        {
            return NotFound();
        }
        _context.Rules.Remove(rule);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

