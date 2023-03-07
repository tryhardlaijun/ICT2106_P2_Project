using System;
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

        public Task<bool> LoadRulesBackup(Guid profileId, IEnumerable<Rule> rules)
        {
            // Get all rules based on profile Id
            // Delete them
            // Iterate through the rules and insert into database
            throw new NotImplementedException();
        }
    }
}

