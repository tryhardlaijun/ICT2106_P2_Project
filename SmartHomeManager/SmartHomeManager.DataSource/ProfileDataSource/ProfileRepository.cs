using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.ProfileDataSource
{
    public class ProfileRepository : IGenericRepository<Profile>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProfileRepository(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<bool> AddAsync(Profile profile)
        {
            try
            {
                await _applicationDbContext.Profiles.AddAsync(profile);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            } catch
            {
                return false;
            }
        }

        public Task<bool> DeleteAsync(Profile entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            return await _applicationDbContext.Profiles.ToListAsync();
        }

        public Task<Profile?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Profile entity)
        {
            throw new NotImplementedException();
        }
    }
}
