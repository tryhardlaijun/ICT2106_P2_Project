using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
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
                EnergyProfile eProfile = new EnergyProfile
                {
                    ConfigurationValue = energyProfile.ConfigurationValue,
                    AccountId = energyProfile.AccountId,
                    ConfigurationDesc = "Some description"
                };
                await _applicationDbContext.EnergyProfiles.AddAsync(eProfile);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding the EnergyProfile: " + ex.Message);
                return false; 
            }
        }

        public async Task<bool> UpdateAsync(EnergyProfile energyProfile)
        {
            try
            {
                var existingEnergyProfile = await _applicationDbContext.EnergyProfiles.FirstOrDefaultAsync(p => p.AccountId == energyProfile.AccountId);

                // If the entity does not exist, return false.
                if (existingEnergyProfile == null)
                {
                    return false; 
                }

                // Update the existing entity properties with the new values
                existingEnergyProfile.ConfigurationValue = energyProfile.ConfigurationValue;

                _applicationDbContext.EnergyProfiles.Update(existingEnergyProfile);
                await _applicationDbContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<EnergyProfile>> GetAllAsync()
        {
            return await _applicationDbContext.EnergyProfiles.Include(p => p.Account).ToListAsync();
        }

        public async Task<EnergyProfile> GetByIdAsync(Guid accountId)
        {
            var energyProfile = await _applicationDbContext.EnergyProfiles.FirstOrDefaultAsync(p => p.AccountId == accountId);
            
            return energyProfile;
        }

        public Task<bool> DeleteAsync(EnergyProfile entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

    }
}
