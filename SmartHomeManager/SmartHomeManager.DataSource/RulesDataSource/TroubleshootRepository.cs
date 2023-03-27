using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SceneDomain.Entities;

namespace SmartHomeManager.DataSource.RulesDataSource
{
    public class TroubleshootRepository : IGenericRepository<Troubleshooter>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        protected DbSet<Troubleshooter> _dbSet;

        public TroubleshootRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = _applicationDbContext.Set<Troubleshooter>();
        }

        public async Task<bool> AddAsync(Troubleshooter troubleshoot)
        {
            try
            {
                await _dbSet.AddAsync(troubleshoot);
                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Troubleshooter troubleshoot)
        {
            try
            {
                _dbSet.Remove(troubleshoot);
                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                Troubleshooter? troubleshooter = await _dbSet.FindAsync(id);
                if (troubleshooter != null)
                {
                    _dbSet.Remove(troubleshooter);
                    return await SaveAsync();
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Troubleshooter>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Troubleshooter?> GetByIdAsync(Guid id)
        {
            try
            {
                var troubleshooter = await _dbSet.FindAsync(id);
                return troubleshooter;
            }
            catch
            {
                return null;
            }
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

        public async Task<bool> UpdateAsync(Troubleshooter troubleshooter)
        {
            try
            {
                _dbSet.Update(troubleshooter);
                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }
       
    }
}
