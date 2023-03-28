using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using SmartHomeManager.Domain.HomeSecurityDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.HomeSecurityDataSource
{
    public class HomeSecurityRepository : IHomeSecurityRepository
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

        public async Task<HomeSecurity?> GetByAccountIdAsync(Guid accountId)
        {
            return await _applicationDbContext.HomeSecurities.Where(r => r.AccountId == accountId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(HomeSecurity homeSecurity)
        {
            try
            {
                _applicationDbContext.Update(homeSecurity);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
