using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.BackupDomain.Interfaces
{
    public interface IBackupScenarioRepository
    {
        public Task<IEnumerable<BackupScenario>> GetAllBackupScenario();
        public Task<List<BackupScenario>> GetBackupScenarioById(Guid scenarioId);
        public Task<bool> CreateBackupScenario(BackupScenario backupScenario);
        public Task<bool> DeleteBackupScenario(BackupScenario backupScenario);
    }
}
