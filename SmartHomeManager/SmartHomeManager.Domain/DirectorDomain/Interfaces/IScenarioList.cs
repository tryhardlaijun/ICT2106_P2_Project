using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.DirectorDomain.Interfaces
{
    public interface IScenarioList
    {
        public IScenarioList Clone();
        public void replaceScenarioList(List<Scenario> scenarioList);
        public List<Scenario> getScenarioList();
    }
}
