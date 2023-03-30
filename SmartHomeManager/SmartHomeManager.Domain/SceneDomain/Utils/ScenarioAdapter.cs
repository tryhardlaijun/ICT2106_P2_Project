using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities.DTOs;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
    public class ScenarioAdapter : IAdapter<Scenario, ScenarioRequest>
    {
        public ScenarioRequest ToRequest(Scenario scenario)
        {
            return new ScenarioRequest
            {
                ScenarioId = scenario.ScenarioId,
                ScenarioName = scenario.ScenarioName,
                ProfileId = scenario.ProfileId,
                isActive = scenario.isActive
            };
        }

        public Scenario ToEntity(ScenarioRequest scenarioRequest)
        {
            return new Scenario
            {
                ScenarioId = scenarioRequest.ScenarioId,
                ScenarioName = scenarioRequest.ScenarioName,
                ProfileId = scenarioRequest.ProfileId,
                isActive = scenarioRequest.isActive
            };
        }
    }
}