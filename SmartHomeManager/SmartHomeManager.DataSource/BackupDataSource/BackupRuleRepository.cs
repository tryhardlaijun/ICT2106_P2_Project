using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.BackupDataSource
{
    public class BackupRuleRepository : IGenericRepository<BackupRule>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BackupRuleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddAsync(BackupRule backupRule)
        {
            return await CreateBackupRule(backupRule);
        }

        public async Task<bool> DeleteAsync(BackupRule entity)
        {
            return await DeleteBackupRule(entity);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BackupRule>> GetAllAsync()
        {
            return await GetAllBackupRule();
        }

        public async Task<BackupRule?> GetByIdAsync(Guid id)
        {
            return await GetBackupRuleById(id);
        }

        public async Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(BackupRule entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BackupRule>> GetAllBackupRule()
        {
            return await _applicationDbContext.BackupRule.ToListAsync();
        }
        public async Task<BackupRule> GetBackupRuleById(Guid ruleId)
        {
            return await _applicationDbContext.BackupRule.FindAsync(ruleId);
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
