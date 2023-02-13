using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.EnergyProfileDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.EnergyProfileDomain.Services
{
    public class EnergyProfileServices
    {
        private readonly IGenericRepository<EnergyProfile> _energyProfileRepository;

        public EnergyProfileServices(IGenericRepository<EnergyProfile> energyProfileRepository)
        {
            _energyProfileRepository = energyProfileRepository;
        }
    }
}
