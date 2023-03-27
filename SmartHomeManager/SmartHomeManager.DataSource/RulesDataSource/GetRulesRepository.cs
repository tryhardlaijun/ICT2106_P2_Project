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

        public async Task<IEnumerable<Rule?>> GetAllRulesByScenarioIdAsync(Guid ScenarioId)
        {
            return await _applicationDbContext.Rules.Where(r => r.ScenarioId == ScenarioId).ToListAsync();
        }
        public async Task<IEnumerable<Rule>> GetSchedulesByScenarioIdAsync(Guid ScenarioId)
        {
            return await _applicationDbContext.Rules.Where(r => r.ScenarioId == ScenarioId && r.StartTime != null).ToListAsync();
        }

        public async Task<IEnumerable<Rule>> GetApiByScenarioIdAsync(Guid ScenarioId)
        {
            return await _applicationDbContext.Rules.Where(r => r.ScenarioId == ScenarioId && r.APIKey != null).ToListAsync();
        }

        public async Task<IEnumerable<Rule>> GetEventsByScenarioIdAsync(Guid ScenarioId)
        {
            return await _applicationDbContext.Rules.Where(r => r.ScenarioId == ScenarioId && r.ActionTrigger != null).ToListAsync();
        }        
    }
}

