using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.Common;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartHomeManager.API.Controllers.ScenariosAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ScenariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Scenarios/GetAllScenarios
        [HttpGet("GetAllScenarios")]
        public async Task<ActionResult<IEnumerable<Scenario>>> GetAllScenarios()
        {
            if (_context.Scenarios == null)
            {
                return NotFound();
            }
            var result = await _context.Rules.ToListAsync();

            return Ok(result);
        }

        // GET api/Scenarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Scenario>> GetScenario([FromBody] Guid id)
        {
            if (_context.Scenarios == null)
            {
                return NotFound();
            }
            var scenario = await _context.Scenarios.FindAsync(id);
            if (scenario == null)
            {
                return NotFound();
            }
            return scenario;
        }

        // POST api/Scenarios
        [HttpPost("CreateScenario")]
        public async Task<ActionResult<Scenario>> CreateScenario([FromBody] Scenario scenario)
        {
            if (_context.Scenarios == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Scenarios'  is null.");
            }
            _context.Scenarios.Add(scenario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetScenario), new { id = scenario.ScenarioId }, scenario);
        }

        // PUT api/Scenarios/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/Scenarios/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScenario([FromBody] Guid id)
        {
            if (_context.Scenarios == null)
            {
                return NotFound();
            }
            var scenario = await _context.Scenarios.FindAsync(id);
            if (scenario == null)
            {
                return NotFound();
            }
            _context.Scenarios.Remove(scenario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

