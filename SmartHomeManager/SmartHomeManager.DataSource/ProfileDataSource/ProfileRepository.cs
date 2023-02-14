using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;

namespace SmartHomeManager.DataSource.ProfileDataSource
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync(Profile profile)
        {
            await _dbContext.Profiles.AddAsync(profile);

            return true;
        } 

        public async Task<bool> DeleteAsync(Profile profile)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Profile?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveAsync()
        {
            int result = await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<bool> UpdateAsync(Profile profile)
        {
            throw new NotImplementedException();
        }
    }
}
