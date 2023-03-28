using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DirectorDomain.Interfaces
{
    public interface IInformDirectorServices
    {
        Task<bool> InformRuleChangesAsync(Guid ruleID, char operation);
        Task<bool> InformScenarioChangesAsync(Guid scenarioID, char operation);
    }
}
