using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DirectorDomain.Entities;
using SmartHomeManager.Domain.DirectorDomain.Services;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using SmartHomeManager.Domain.HomeSecurityDomain.Services;

namespace SmartHomeManager.API.HomeSecurityAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeSecurityController : ControllerBase
    {
        private readonly HomeSecurityServices _homeSecurityService;

        /*public HomeSecurityController(IGenericRepository<HomeSecurity> homeSecurityRepo)
        {
            _homeSecurityService = new(homeSecurityRepo);
        }

        // GET: api/HomeSecurity
        [HttpGet("GetHomeSecurity")]
        public async Task<IEnumerable<HomeSecurity>> GetHomeSecurity()
        {
            return await _homeSecurityService.GetAllRulesAsync();
        }*/
    }
}
