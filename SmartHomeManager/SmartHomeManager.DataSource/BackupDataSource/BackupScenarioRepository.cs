using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.BackupDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.BackupDataSource
{
    public class BackupScenarioRepository : IBackupScenarioRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public BackupScenarioRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<BackupScenario>> GetAllBackupScenario()
        {
            return await _applicationDbContext.BackupScenarios.ToListAsync();
        }
        public async Task<List<BackupScenario>> GetBackupScenarioById(Guid profileId)
        {
            return await _applicationDbContext.BackupScenarios.Where(s => s.ProfileId == profileId).ToListAsync();
        }
        public async Task<bool> CreateBackupScenario(BackupScenario backupScenario)
        {
            try
            {
                await _applicationDbContext.BackupScenarios.AddAsync(backupScenario);
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
