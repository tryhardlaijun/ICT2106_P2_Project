using System;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.SceneDomain.Entities;
namespace SmartHomeManager.DataSource.RulesDataSource
{
	public class BackupScenarioRepository
	{
        private readonly ApplicationDbContext _applicationDbContext;
        public BackupScenarioRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> LoadRulesBackup(Guid profileId, IEnumerable<Domain.SceneDomain.Entities.Scenario> rules)
        {
            IEnumerable<Domain.SceneDomain.Entities.Scenario> allScenarios = await _applicationDbContext.Scenarios.ToListAsync();
            foreach (var scenario in allScenarios)
            {
                _applicationDbContext.Scenarios.Remove(scenario);
                await _applicationDbContext.SaveChangesAsync();
            }
            foreach (var scenario in allScenarios)
            {
                await _applicationDbContext.AddRangeAsync(scenario);
                await _applicationDbContext.SaveChangesAsync();
            }
            return true;
        }
    }
}

