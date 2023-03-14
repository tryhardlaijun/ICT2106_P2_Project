using SmartHomeManager.Domain.SceneDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.SceneDomain.Interfaces
{
    public interface IGetTroubleshooterService
    {
        // get all rules on startup and when director is informed
        Task<IEnumerable<Troubleshooter>> GetAllTroubleshooter();

        Task<Troubleshooter> GetTroubleshooterById(Guid id);
    }
}
