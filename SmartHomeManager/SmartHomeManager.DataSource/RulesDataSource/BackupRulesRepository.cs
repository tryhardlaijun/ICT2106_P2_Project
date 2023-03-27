using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.DataSource.RulesDataSource
{
	public class BackupRulesRepository: IBackupRulesRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
		public BackupRulesRepository(ApplicationDbContext applicationDbContext)
		{
            _applicationDbContext = applicationDbContext;
		}

        public async Task<bool> LoadRulesBackup(Guid profileId, IEnumerable<Domain.SceneDomain.Entities.Rule> rules)
        {
            // Get all rules based on profile Id
            IEnumerable<Domain.SceneDomain.Entities.Rule> allRules = await _applicationDbContext.Rules.Include(d => d.Device).Include(s => s.Scenario).AsNoTracking().ToListAsync();
            // Delete them
            foreach(var rule in allRules)
            {
                 _applicationDbContext.Rules.Remove(rule);
                await _applicationDbContext.SaveChangesAsync();
            }
            foreach(var rule in allRules)
            {
                await _applicationDbContext.AddRangeAsync(rule);
                await _applicationDbContext.SaveChangesAsync();
            }
            return true;
        }        
    }
}

