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
            _applicationDbContext.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }

        public async Task<bool> DeleteRule(Domain.SceneDomain.Entities.Rule rule)
        {
            _applicationDbContext.Rules.Remove(rule);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateRule(Domain.SceneDomain.Entities.Rule rule)
        {
            await _applicationDbContext.AddRangeAsync(rule);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}

