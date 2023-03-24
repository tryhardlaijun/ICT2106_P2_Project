using System;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.RoomDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Interfaces;

namespace SmartHomeManager.DataSource.RulesDataSource
{
    public class RuleRepository : IGenericRepository<Rule>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        protected DbSet<Rule> _dbSet;
        public RuleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            this._dbSet = _applicationDbContext.Set<Rule>();
        }

        #region IGeneric Region
        // Add rule
        public async Task<bool> AddAsync(Rule rule)
        {
            try
            {
                await _applicationDbContext.AddRangeAsync(rule);
                return await SaveAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        // Get all
        public async Task<IEnumerable<Rule>> GetAllAsync()
        {
            try
            {
                return await _applicationDbContext.Rules.Include(d => d.Device).Include(s => s.Scenario).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        //Get by Id
        public async Task<Rule?> GetByIdAsync(Guid id)
        {
            try
            {
                var rule = await _applicationDbContext.Rules.Include(d => d.Device).Include(s => s.Scenario).FirstOrDefaultAsync(r => r.RuleId == id);
                return rule;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Rule> GetRuleByIdAsync(Guid id)
        {
            try
            {
                var rule = await _applicationDbContext.Rules.Include(d => d.Device).Include(s => s.Scenario).FirstOrDefaultAsync(r => r.RuleId == id);
                return rule;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //Update
        public async Task<bool> UpdateAsync(Rule rule)
        {
            try
            {
                _applicationDbContext.Update(rule);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        // Delete by Id
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                Rule? rule = await _applicationDbContext.Rules.FindAsync(id);
                if(rule != null)
                {
                    _applicationDbContext.Rules.Remove(rule);
                    return await SaveAsync();
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }           
        }

        //Save
        public async Task<bool> SaveAsync()
        {
            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false; 
            }
        }

        public Task<bool> DeleteAsync(Rule entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Provided Inteface
        public async Task<IEnumerable<Rule>> GetAllRulesByScenarioIdAsync(Guid ScenarioId)
        {
            try
            {
                return await _applicationDbContext.Rules.Where(r => r.ScenarioId == ScenarioId).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }
        #endregion
    }
}
