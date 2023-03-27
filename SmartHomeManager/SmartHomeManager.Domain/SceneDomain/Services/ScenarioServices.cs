using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
	public class ScenarioServices
	{
		private readonly IScenarioRepository<Scenario> _scenarioRepository;
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
		
        //Get using Name
        public async Task<Scenario?> GetScenarioByName(string name)
        {
            return await _scenarioRepository.GetByNameAsync(name);
        }

        //Check voice input
        public async Task<bool> CheckVoiceInput(string input)
            {
                if (input.StartsWith("Create"))
                {
                    string[] inputArray = input.Split(" ");
                    if (inputArray.Length > 1)
                    {
                        // string scenarioNumberId = "3fa85f64-5717-4562-b3fc-2c963f66afa7";
                        // Guid guidScenarioIdValue = Guid.Parse(scenarioNumberId);
                        
                        Guid guidScenarioIdValue = Guid.NewGuid();

                        string profileId = "22222222-2222-2222-2222-222222222222";
                        Guid guidProfileIdValue = Guid.Parse(profileId);

                        // Create a new scenario
                        Scenario createScenario = new Scenario
                        {
                            ScenarioId = guidScenarioIdValue,
                            ScenarioName = inputArray[1],
                            ProfileId = guidProfileIdValue,
                            isActive = false
                        };

                        if (await CreateScenarioAsync(createScenario))
                        {
                            return true;
                        }
                    }
                }
                else if (input.StartsWith("Update"))
                {
                    string[] inputArray = input.Split(" ");
                    if (inputArray.Length == 4)
                    {
                        string oldScenarioName = inputArray[1];
                        string newScenarioName = inputArray[3];
                        
                        // Get old scenario name
                        var scenario = await GetScenarioByName(oldScenarioName);
                        
                        // Update to new input scenario name
                        scenario.ScenarioName = newScenarioName;
                        
                        if (await EditScenarioAsync(scenario))
                        {
                            return true;
                        }
                    }
                }
                else if (input.StartsWith("Delete"))
                {
                    string[] inputArray = input.Split(" ");
                    if (inputArray.Length > 1)
                    {
                        string scenarioName = inputArray[1];

                        // Get the scenario by name
                        var scenarioToDelete = await _scenarioRepository.GetByNameAsync(scenarioName);
                        
                        // Delete the scenario by id
                        if (await DeleteScenarioeByIdAsync(scenarioToDelete.ScenarioId))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return false;
                }
                return false;
            }
		
    }
}

