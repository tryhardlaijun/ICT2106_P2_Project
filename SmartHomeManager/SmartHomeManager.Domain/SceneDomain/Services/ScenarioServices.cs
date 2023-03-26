using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class ScenarioServices
	{
		private readonly IGenericRepository<Scenario> _scenarioRepository;
        private readonly IInformDirectorServices _informDirectorServices;
		public ScenarioServices(IGenericRepository<Scenario> scenarioRepository, IInformDirectorServices informDirectorServices)
		{
			_scenarioRepository = scenarioRepository;
            _informDirectorServices = informDirectorServices;
		}

        //Get all
        public async Task<IEnumerable<Scenario>> GetAllScenariosAsync()
        {
            return await _scenarioRepository.GetAllAsync();
        }

        //Get using id
        public async Task<Scenario?> GetScenarioByIdAsync(Guid id)
        {
            return await _scenarioRepository.GetByIdAsync(id);
        }

        //Create
        public async Task<bool> CreateScenarioAsync(Scenario scenario)
        {
            if(await _scenarioRepository.AddAsync(scenario) == true)
            {
                _informDirectorServices.InformScenarioChangesAsync(scenario.ScenarioId, 'c');
                return true;
            }

            return false;     
        }

        //Update
    	public async Task<bool> EditScenarioAsync(Scenario scenario)
		{
            if (await _scenarioRepository.UpdateAsync(scenario))
            {
                _informDirectorServices.InformScenarioChangesAsync(scenario.ScenarioId, 'u');
                return true;
            }

            return false;
        }

        //Delete using Id
		public async Task<bool> DeleteScenarioeByIdAsync(Guid id)
		{
			if (await _scenarioRepository.DeleteByIdAsync(id))
			{
                _informDirectorServices.InformScenarioChangesAsync(id, 'd');
				return true;

            }
            
			return false;
        }
    }
}

