using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.BackupDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.BackupDomain.Services
{
    public class BackupServices : IUpdateBackupService
    {
        private readonly IGenericRepository<BackupRule> _backupRuleRepository;
        private readonly IGenericRepository<BackupScenario> _backupScenarioRepository;
        public BackupServices(IGenericRepository<BackupRule> backupRuleRepository, IGenericRepository<BackupScenario> backupScenarioRepository)
        {
            _backupRuleRepository = backupRuleRepository;
            _backupScenarioRepository = backupScenarioRepository;
        }
        public async Task<BackupRule> loadBackupRule(Guid backupRuleId)
        {
            return await _backupRuleRepository.GetByIdAsync(backupRuleId);
        }
        public async Task<BackupScenario> loadBackupScenario(Guid backupScenarioId)
        {
            return await _backupScenarioRepository.GetByIdAsync(backupScenarioId);
        }
        //to add restoreBackupComplete() after writing interface for IUpdateBackupService
    }
}
