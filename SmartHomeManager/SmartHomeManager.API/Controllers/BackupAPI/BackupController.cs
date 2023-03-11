using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.BackupDomain.Interfaces;
using SmartHomeManager.Domain.BackupDomain.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.API.Controllers.BackupAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        private readonly BackupServices _backupServices;

        public BackupController(IBackupRuleRepository backupRuleRepo, IBackupScenarioRepository backupScenarioRepo)
        {
            _backupServices = new(backupRuleRepo, backupScenarioRepo);
        }

        [HttpGet("loadBackupRule/{scenarioId}")]
        public async Task<List<BackupRule>> loadBackupRule(Guid scenarioId) //public async Task<List<Rule>> loadBackupRule(Guid scenarioId)
        {
            return await _backupServices.loadBackupRule(scenarioId);
        }

        [HttpGet("loadBackupScenario/{profileId}")]
        public async Task<List<BackupScenario>> loadBackupScenario(Guid profileId) //public async Task<List<Scenario>> loadBackupScenario(Guid profileId)
        {
            return await _backupServices.loadBackupScenario(profileId);
        }

        [HttpGet("getAllBackupScenario")]
        public async Task<IEnumerable<BackupScenario>> getAllBackupScenario()
        {
            return await _backupServices.getAllBackupScenario();
        }

        [HttpGet("getAllBackupRule")]
        public async Task<IEnumerable<BackupRule>> getAllBackupRule()
        {
            return await _backupServices.getAllBackupRule();
        }
    }
}
