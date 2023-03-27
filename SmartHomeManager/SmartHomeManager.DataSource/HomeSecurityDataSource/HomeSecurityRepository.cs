using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.HomeSecurityDataSource
{
    public class HomeSecurityRepository : IGenericRepository<HomeSecurity>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeSecurityRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> AddAsync(HomeSecurity homeSecurity)
        {
            try
            {
                await _applicationDbContext.HomeSecurities.AddAsync(homeSecurity);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(HomeSecurity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HomeSecurity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<HomeSecurity?> GetByIdAsync(Guid homeSecurityId)
        {
            return await _applicationDbContext.HomeSecurities.Where(r => r.HomeSecurityId == homeSecurityId).LastAsync();
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(HomeSecurity homeSecurity)
        {
            try
            {
                _applicationDbContext.Update(homeSecurity);
                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }
        
        public async Task<HomeSecurity?> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
        
    }
}
