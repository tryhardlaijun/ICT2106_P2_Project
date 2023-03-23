using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.APIDomain.Entities;
using SmartHomeManager.Domain.APIDomain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.APIDataSource
{
	public class APIValueRepository : IAPIValueRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;
		public APIValueRepository(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public Task<List<APIValue>> GetAPIValueById(Guid APIDataId)
		{
			throw new NotImplementedException();
		}
		/*
public async Task<List<APIValue>> GetAPIValueById(Guid APIDataId)
{
	return await _applicationDbContext.APIValues.Where(s => s.APIKeyType == APIDataId).ToListAsync();
}*/
	}
}
