using SmartHomeManager.Domain.DirectorDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Services
{
    public class ScenarioList : IScenarioList
    {
        public List<Scenario> scenarioList;
        public ScenarioList(List<Scenario> scenarioList)
        {
            this.scenarioList = scenarioList;
        }

        public IScenarioList Clone()
        {
            List<Scenario> cloneList = scenarioList.ConvertAll(r => new Scenario
            {
                ScenarioId = r.ScenarioId,
                ScenarioName = r.ScenarioName,
                Profile = r.Profile,
                isActive = r.isActive
            }).ToList();
            ScenarioList clone = new ScenarioList(cloneList);
            return clone;
        }

        public List<Scenario> getScenarioList()
        {
            return scenarioList;
        }

        public void replaceScenarioList(List<Scenario> scenarioList)
        {
            this.scenarioList = scenarioList;
        }
    }
}
