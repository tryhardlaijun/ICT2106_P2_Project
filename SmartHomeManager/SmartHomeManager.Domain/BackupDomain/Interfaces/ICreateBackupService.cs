using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.BackupDomain.Interfaces
{
    public interface ICreateBackupService
    {
        void createBackup(List<Rule> rulesList, List<Scenario> scenarioList);
    }
}
