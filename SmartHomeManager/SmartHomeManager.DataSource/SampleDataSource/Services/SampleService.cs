using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SampleDomain.Entities;

namespace SmartHomeManager.DataSource.SampleDataSource.Services
{
    public class SampleService 
    {
        private readonly IGenericRepository<Sample> _sampleRepository;

        public SampleService(IGenericRepository<Sample> sampleRepository) 
        {
            _sampleRepository = sampleRepository;
        }

        public async Task<string> GetNameByIdAsync(Guid id)
        {
            Sample? s = await _sampleRepository.GetByIdAsync(id);
            if (s is null)
            {
                return String.Empty;
            }

            return s.Name;
        }
    }
}
