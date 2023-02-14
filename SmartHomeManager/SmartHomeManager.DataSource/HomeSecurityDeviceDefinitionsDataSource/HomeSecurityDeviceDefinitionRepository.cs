using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using SmartHomeManager.Domain.HomeSecurityDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.HomeSecurityDeviceDefinitionsDataSource
{
    public class HomeSecurityDeviceDefinitionRepository : IHomeSecurityDeviceDefinitionRepository<HomeSecurityDeviceDefinition>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeSecurityDeviceDefinitionRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<HomeSecurityDeviceDefinition?> GetByDeviceGroup(string deviceGroup)
        {
            return await _applicationDbContext.HomeSecurityDeviceDefinitions.Where(r => r.DeviceGroup == deviceGroup).LastAsync();
        }
    }
}
