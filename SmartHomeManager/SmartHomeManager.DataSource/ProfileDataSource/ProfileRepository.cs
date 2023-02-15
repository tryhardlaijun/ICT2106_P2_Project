using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Entities;

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

        public async Task<IEnumerable<Guid>> GetDevicesByProfileId(Guid profileId)
        {
            /*return new List<Guid>();*/

            var a = (await _dbContext.DeviceProfiles.ToListAsync()).Where(p => p.ProfileId == profileId).Select(p => p.DeviceId).ToList();
            if (a.Count >= 0)
                return a;
            /*Profile? test = await _dbContext.Profiles.Where(profile => profile.ProfileId == profileId).FirstOrDefaultAsync() as Profile;
            if (test != null) {
                return test.DeviceProfiles.Select(deviceProfile => deviceProfile.DeviceId).ToList();
            }*/


            return Enumerable.Empty<Guid>();

            // var test = _dbContext.DeviceProfile.ToList().Where(o => o.ProfilesProfileId == profileId);
            /*IEnumerable<Guid> array = Enumerable.Empty<Guid>();*/
            /*            foreach (var t in test)
                        {
                            array.Append<Guid>(t.DevicesDeviceId);
                        }

            //_dbContext.DeviceProfile.Where(deviceProfile => deviceProfile == profileId);

            return Array<Guid>.Empty.ToList();*/
        }
    }
}
