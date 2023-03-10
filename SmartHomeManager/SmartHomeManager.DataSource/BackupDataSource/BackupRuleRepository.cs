using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.BackupDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.BackupDataSource
{
    public class BackupRuleRepository : IBackupRuleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BackupRuleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<BackupRule>> GetAllBackupRule()
        {
            return await _applicationDbContext.BackupRule.ToListAsync();
        }
        public async Task<List<BackupRule>> GetBackupRuleById(Guid scenarioId)
        {
            return await _applicationDbContext.BackupRule.Where(r => r.scenarioID == scenarioId).ToListAsync();
        }
        public async Task<bool> CreateBackupRule(BackupRule backupRule)
        {
            try
            {
                await _applicationDbContext.BackupRule.AddAsync(backupRule);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteBackupRule(BackupRule backupRule)
        {
            try
            {
                _applicationDbContext.Remove(backupRule);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
