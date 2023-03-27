using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.SceneDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.Domain.SceneDomain.Services
{
    public class TroubleshooterServices : ITroubleshooterServices
    {

        private readonly IGenericRepository<Troubleshooter> _troubleshooterRepository;

        public TroubleshooterServices(IGenericRepository<Troubleshooter> troubleshooterRepository)
        {
            _troubleshooterRepository = troubleshooterRepository;
        }

        public async Task<IEnumerable<Troubleshooter>> GetTroubleshootersAsync()
        {
            return await _troubleshooterRepository.GetAllAsync();
        }

        public void Update(Guid deviceId, string configKey, int configVal)
        {
            // trigger here
            Console.WriteLine("Updating Troubleshooter Service");
        }
    }
}
