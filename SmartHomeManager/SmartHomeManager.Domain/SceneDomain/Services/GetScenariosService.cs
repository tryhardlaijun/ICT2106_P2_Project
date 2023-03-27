using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Services;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class GetScenariosService : IGetScenariosService
    {
        private readonly IScenarioRepository<Scenario> _scenarioRepository;
        public GetScenariosService(IScenarioRepository<Scenario> scenarioRepository)
		{
            _scenarioRepository = scenarioRepository;
		}

        public async  Task<IEnumerable<Scenario>> GetAllScenarios()
        {
            return await _scenarioRepository.GetAllAsync();
        }

        public async Task<Scenario?> GetScenarioById(Guid id)
        {
            return await _scenarioRepository.GetByIdAsync(id);
        }

    }
}

