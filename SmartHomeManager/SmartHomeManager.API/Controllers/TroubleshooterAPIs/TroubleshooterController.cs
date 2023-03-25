using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.SceneDomain.Entities.DTOs;
using SmartHomeManager.Domain.SceneDomain.Services;
using SmartHomeManager.Domain.Common;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.DataSource;
using SmartHomeManager.DataSource.RulesDataSource;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.API.Controllers.TroubleshooterAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class TroubleshooterController : ControllerBase
    {
       
        private readonly TroubleshooterServices _troubleshooterServices;

        public TroubleshooterController(IGenericRepository<Troubleshooter> troubleshooterRepository)
        {
            _troubleshooterServices = new(troubleshooterRepository);
        }
        

        // GET: api/Troubleshooter/GetAllTroubleshooters
        [HttpGet("GetAllTroubleshooter")]
        public async Task<IEnumerable<TroubleshooterRequest>> GetAllTroubleshooter()
        {
            var troubleshooter = await _troubleshooterServices.GetTroubleshootersAsync();

   
            var resp = troubleshooter.Select(troubleshooter => new TroubleshooterRequest
            {
                TroubleshooterId = troubleshooter.TroubleshooterId,
                Recommendation = troubleshooter.Recommendation,
                DeviceType = troubleshooter.DeviceType,
                ConfigurationKey = troubleshooter.ConfigurationKey
            }).ToList();
            return resp;
        }

       

    }
}
