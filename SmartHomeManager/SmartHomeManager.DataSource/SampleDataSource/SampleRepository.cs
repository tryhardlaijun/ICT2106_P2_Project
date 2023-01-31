using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SampleDomain.Entities;

namespace SmartHomeManager.DataSource.SampleDataSource
{
    public class SampleRepository : IGenericRepository<Sample>
    {
        private readonly SampleDbContext _sampleDbContext;

        public SampleRepository(SampleDbContext sampleDbContext)
        {
            _sampleDbContext = sampleDbContext;
        }

        public Task<bool> AddAsync(Sample entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Sample entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Sample>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Sample?> GetByIdAsync(Guid id)
        {
            return await _sampleDbContext.Samples.FindAsync(id);    
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Sample entity)
        {
            throw new NotImplementedException();
        }
    }
}
