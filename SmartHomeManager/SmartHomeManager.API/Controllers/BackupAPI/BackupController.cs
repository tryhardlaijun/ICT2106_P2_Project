﻿using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.BackupDomain.Entities;
using SmartHomeManager.Domain.BackupDomain.Interfaces;
using SmartHomeManager.Domain.BackupDomain.Services;
using SmartHomeManager.Domain.Common;

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
        public async Task<List<BackupRule>> loadBackupRule(Guid scenarioId)
        {
            return await _backupServices.loadBackupRule(scenarioId);
        }
        
        [HttpGet("loadBackupScenario/{profileId}")]
        public async Task<List<BackupScenario>> loadBackupScenario(Guid profileId)
        {
            return await _backupServices.loadBackupScenario(profileId);
        }
    }
}
