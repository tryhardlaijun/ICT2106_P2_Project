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
    }
}

