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
        public async Task<bool> DeleteScenario(Scenario scenario)
        {
            _applicationDbContext.Scenarios.Remove(scenario);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateScenario(Scenario scenario)
        {
            await _applicationDbContext.AddRangeAsync(scenario);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}

