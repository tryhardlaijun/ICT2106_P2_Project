using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class GetScenariosService : IGetScenariosService
	{
        private readonly IGenericRepository<Scenario> _scenarioRepository;
        public GetScenariosService(IGenericRepository<Scenario> scenarioRepository)
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

