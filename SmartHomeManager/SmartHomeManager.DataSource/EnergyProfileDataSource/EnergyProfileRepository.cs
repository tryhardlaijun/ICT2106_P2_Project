using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.EnergyProfileDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.EnergyProfileDataSource
{

    public class EnergyProfileRepository : IGenericRepository<EnergyProfile>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EnergyProfileRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddAsync(EnergyProfile energyProfile)
        {
            try
            {
                await _applicationDbContext.EnergyProfiles.AddAsync(energyProfile);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> DeleteAsync(EnergyProfile entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EnergyProfile>> GetAllAsync()
        {
            return await _applicationDbContext.EnergyProfiles.Include(r => r.Account).ToListAsync();
        }

        public Task<EnergyProfile?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(EnergyProfile entity)
        {
            throw new NotImplementedException();
        }
    }
}
