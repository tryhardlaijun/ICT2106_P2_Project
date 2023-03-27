using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.EnergyProfileDomain.Entities;
using SmartHomeManager.Domain.EnergyProfileDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.EnergyProfileDataSource
{

    public class EnergyProfileRepository : IEnergyProfileRepository<EnergyProfile>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EnergyProfileRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _applicationDbContext.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
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

        public async Task<bool> UpdateConfigValueAsync(Guid accountId, int configValue)
        {
            try
            {
                var existingEnergyProfile = await _applicationDbContext.EnergyProfiles.FirstOrDefaultAsync(p => p.AccountId == accountId);

                // If the entity does not exist, return false.
                if (existingEnergyProfile == null)
                {
                    EnergyProfile ep = new EnergyProfile();
                    ep.EnergyProfileId = Guid.NewGuid();
                    ep.ConfigurationValue = configValue;
                    ep.AccountId = accountId;
                    ep.ConfigurationDesc = "Test run";
                    await _applicationDbContext.EnergyProfiles.AddAsync(ep);
                    await _applicationDbContext.SaveChangesAsync();
                    return false;
                }

                // Update the existing entity properties with the new values
                existingEnergyProfile.ConfigurationValue = configValue;

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

        public async Task<EnergyProfile?> GetByIdAsync(Guid accountId)
        {
            var existingEnergyProfile = await _applicationDbContext.EnergyProfiles.FirstOrDefaultAsync(p => p.AccountId == accountId);
            // If the entity does not exist, return false.
            if (existingEnergyProfile == null)
            {
                EnergyProfile ep = new EnergyProfile();
                ep.EnergyProfileId = Guid.NewGuid();
                ep.ConfigurationValue = 0;
                ep.AccountId = accountId;
                ep.ConfigurationDesc = "Test run";
                await _applicationDbContext.EnergyProfiles.AddAsync(ep);
                await _applicationDbContext.SaveChangesAsync();
                return ep;
            }
            //var energyProfile = await _applicationDbContext.EnergyProfiles.FirstOrDefaultAsync(p => p.AccountId == accountId);
            
            return existingEnergyProfile!;
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
