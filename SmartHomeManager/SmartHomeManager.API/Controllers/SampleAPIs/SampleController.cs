using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.DataSource.SampleDataSource.Services;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.SampleDomain.Entities;

namespace SmartHomeManager.API.Controllers.SampleAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly SampleService _sampleService;

        public SampleController(IGenericRepository<Sample> sampleRepository)
        {
            _sampleService = new(sampleRepository);   
        }

        [HttpGet("{id}")]
        public async Task<string> GetNameById(Guid id)
        {
            return await _sampleService.GetNameByIdAsync(id);
        }
    }
}
