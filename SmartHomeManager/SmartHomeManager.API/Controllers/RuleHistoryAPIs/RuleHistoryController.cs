using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Services;

namespace SmartHomeManager.API.Controllers.RuleHistoryAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleHistoryController : ControllerBase
    {
        private readonly RuleHistoryServices _ruleHistoryService;

        public RuleHistoryController(IGenericRepository<RuleHistory> ruleHistoryRepository)
        {
            _ruleHistoryService = new(ruleHistoryRepository);
        }

        // GET: api/RuleHistory
        [HttpGet("GetAllRules")]
        public async Task<IEnumerable<RuleHistory>> GetRuleHistories()
        {
            /*if (_ruleHistoryService.RuleHistor == null)
            {
                return NotFound();
            }*/
            return await _ruleHistoryService.GetAllRulesAsync();
        }



        /*// GET: api/RuleHistory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RuleHistory>> GetRuleHistory(Guid id)
        {
            if (_context.RuleHistories == null)
            {
                return NotFound();
            }
            var ruleHistory = await _context.RuleHistories.FindAsync(id);

            if (ruleHistory == null)
            {
                return NotFound();
            }

            return ruleHistory;
        }

        // PUT: api/RuleHistory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRuleHistory(Guid id, RuleHistory ruleHistory)
        {
            if (id != ruleHistory.RuleHistoryId)
            {
                return BadRequest();
            }

            _context.Entry(ruleHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RuleHistoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RuleHistory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RuleHistory>> PostRuleHistory(RuleHistory ruleHistory)
        {
            if (_context.RuleHistories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RuleHistories'  is null.");
            }
            _context.RuleHistories.Add(ruleHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRuleHistory", new { id = ruleHistory.RuleHistoryId }, ruleHistory);
        }

        // DELETE: api/RuleHistory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuleHistory(Guid id)
        {
            if (_context.RuleHistories == null)
            {
                return NotFound();
            }
            var ruleHistory = await _context.RuleHistories.FindAsync(id);
            if (ruleHistory == null)
            {
                return NotFound();
            }

            _context.RuleHistories.Remove(ruleHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        /*private bool RuleHistoryExists(Guid id)
        {
            return (_ruleHistoryService.RuleHistories?.Any(e => e.RuleHistoryId == id)).GetValueOrDefault();
        }*/
    }
}
