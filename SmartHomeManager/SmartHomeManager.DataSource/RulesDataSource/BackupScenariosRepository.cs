using System;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.DataSource.RulesDataSource
{
	public class BackupScenariosRepository : IBackupScenariosRepository
	{
        private readonly ApplicationDbContext _applicationDbContext;
        public BackupScenariosRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public async Task<bool> LoadScenariosBackup(Guid profileId, IEnumerable<Scenario> rules)
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

