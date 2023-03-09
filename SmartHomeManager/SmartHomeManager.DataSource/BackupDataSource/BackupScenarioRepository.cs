using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.BackupDataSource
{
    public class BackupScenarioRepository : IGenericRepository<BackupScenario>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BackupScenarioRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<bool> AddAsync(BackupScenario backupScenario)
        {
            return await CreateBackupScenario(backupScenario);
        }

        public async Task<bool> DeleteAsync(BackupScenario entity)
        {
            return await DeleteBackupScenario(entity);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BackupScenario>> GetAllAsync()
        {
            return await GetAllBackupScenario();
        }

        public async Task<BackupScenario?> GetByIdAsync(Guid id)
        {
            return await GetBackupScenarioById(id);
        }

        public async Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(BackupScenario entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BackupScenario>> GetAllBackupScenario()
        {
            return await _applicationDbContext.BackupScenario.ToListAsync();
        }
        public async Task<BackupScenario> GetBackupScenarioById(Guid scenarioId)
        {
            return await _applicationDbContext.BackupScenario.FindAsync(scenarioId);
        }
        public async Task<bool> CreateBackupScenario(BackupScenario backupScenario)
        {
            try
            {
                await _applicationDbContext.BackupScenario.AddAsync(backupScenario);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteBackupScenario(BackupScenario backupScenario)
        {
            try
            {
                _applicationDbContext.Remove(backupScenario);
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
