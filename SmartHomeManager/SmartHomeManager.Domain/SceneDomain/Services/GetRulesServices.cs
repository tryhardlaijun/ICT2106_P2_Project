﻿using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.Common;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class GetRulesServices: IGetRulesService, IBackupRulesService
    {
        private readonly IGenericRepository<Rule> _ruleRepository;
        public GetRulesServices(IGenericRepository<Rule> ruleRepository)
		{
            _ruleRepository = ruleRepository;
		}
        
        public async Task<IEnumerable<Rule>> GetAllRules()
        {
            return await _ruleRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Rule>> GetAllRulesByScenarioId(Guid ScenarioId)
        {
            throw new NotImplementedException();
        }

        public async Task<Rule?> GetRuleById(Guid id)
        {
            return await _ruleRepository.GetByIdAsync(id);
        }

        public async Task<bool> loadRulesBackup(Guid profileId, IEnumerable<Rule> rules)
        {
            Console.WriteLine("Loaded Rules Backup");
            return true;
        }
    }
}

