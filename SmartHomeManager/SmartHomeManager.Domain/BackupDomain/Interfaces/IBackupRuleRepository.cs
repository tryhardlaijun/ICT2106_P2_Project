using SmartHomeManager.Domain.BackupDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.BackupDomain.Interfaces
{
    public interface IBackupRuleRepository
    {
        public Task<List<BackupRule>> GetAllBackupRuleByBackupId(Guid backupId);
        public Task<bool> CreateBackupRule(BackupRule backupRule);
        public Task<bool> DeleteBackupRule(BackupRule backupRule);
    }
}
