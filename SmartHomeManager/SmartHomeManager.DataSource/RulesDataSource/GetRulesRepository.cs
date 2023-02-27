using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
namespace SmartHomeManager.DataSource.RulesDataSource
{
    // Provided interface's repository
	public class GetRulesRepository : IGetRulesRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;
		public GetRulesRepository(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

        public async Task<IEnumerable<Rule>> GetAllRulesAsync()
        {
            return await _applicationDbContext.Rules.Include(d => d.Device).Include(s => s.Scenario).ToListAsync();
        }

        public async Task<IEnumerable<Rule?>> GetAllRulesByScenarioIdAsync(Guid ScenarioId)
        {
            return await _applicationDbContext.Rules.Where(r => r.ScenarioId == ScenarioId).ToListAsync();
        }

        public async Task<Rule> GetRuleByIdAsync(Guid id)
        {
            try
            {
                var rule = await _applicationDbContext.Rules.Include(d => d.Device).Include(s => s.Scenario).FirstOrDefaultAsync(r => r.RuleId == id);
                return rule;
            }
            catch
            {
                return null;
            }
        }
    }
}

