using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class ScenarioServices : IBackupScenariosService
    {
		private readonly IGenericRepository<Scenario> _scenarioRepository;
		public ScenarioServices(IGenericRepository<Scenario> scenarioRepository)
		{
			_scenarioRepository = scenarioRepository;
		}

        public async Task<IEnumerable<Scenario>> GetAllScenariosAsync()
        {
            return await _scenarioRepository.GetAllAsync();
        }

        public async Task<Scenario?> GetScenarioByIdAsync(Guid id)
        {
            return await _scenarioRepository.GetByIdAsync(id);
        }
        public async Task<bool> loadScenarioBackup(Guid profileID, IEnumerable<Scenario> scenarios)
        {
            return true;
        }
    }
}

