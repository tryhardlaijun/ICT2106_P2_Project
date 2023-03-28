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
    public class HomeSecurityDeviceDefinitionRepository : IHomeSecurityDeviceDefinitionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeSecurityDeviceDefinitionRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<HomeSecurityDeviceDefinition>?> GetAllAsync()
        {
            return await _applicationDbContext.HomeSecurityDeviceDefinitions.ToListAsync();
        }
    }
}
