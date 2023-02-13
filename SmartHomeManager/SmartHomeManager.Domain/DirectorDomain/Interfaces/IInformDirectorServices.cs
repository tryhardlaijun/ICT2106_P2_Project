using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Interfaces
{
    internal interface IInformDirectorServices
    {
        void InformRuleChanges(Guid ruleID, char operation);
        void InformScenarioChanges(Guid scenarioID, char operation);
    }
}
