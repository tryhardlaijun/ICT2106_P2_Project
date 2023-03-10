using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
    public interface IBackupRulesService
    {
        void loadRulesBackup(Guid profileID, IEnumerable<Rule> rules);
    }
}
