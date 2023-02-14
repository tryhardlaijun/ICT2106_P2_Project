using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Interfaces
{
    public interface IInformDirectorServices
    {
        void InformRuleChangesAsync(Guid ruleID, char operation);
        void InformScenarioChangesAsync(Guid scenarioID, char operation);
    }
}
