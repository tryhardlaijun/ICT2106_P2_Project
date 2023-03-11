using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
    public interface IBackupScenarioService
    {
        void loadScenarioBackup(Guid profileID, IEnumerable<Scenario> scenarios);
        //Task<List<Scenario>> loadScenarioBackup(Guid profileID, IEnumerable<Scenario> scenarios);
    }
}
