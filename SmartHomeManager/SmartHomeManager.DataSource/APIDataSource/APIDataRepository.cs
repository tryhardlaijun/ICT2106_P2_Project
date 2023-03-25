using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartHomeManager.Domain.APIDomain.Entities;
using SmartHomeManager.Domain.APIDomain.Service;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.APIDataSource
{
	public class APIDataRepository : IAPIDataRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;
		public APIDataRepository(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}
		public async Task<bool> CreateAPIData(APIData apiData)
		{
			try
			{
				await _applicationDbContext.APIDatas.AddAsync(apiData);
				await _applicationDbContext.SaveChangesAsync();
				
				return true;
			}
			catch
			{
				return false;
			}

		}

		public async Task<IEnumerable<APIData>> GetAPIType(String Type)
		{
			return await _applicationDbContext.APIDatas.Where(s => s.Type == Type).ToListAsync();
		}

		public async Task<List<APIData>> GetAPIDataById(Guid APIDataId)
		{
			return await _applicationDbContext.APIDatas.Where(s => s.APIDataId == APIDataId).ToListAsync();
		}
		public async Task<IEnumerable<APIData>> GetAllAPIData()
		{
			return await _applicationDbContext.APIDatas.ToListAsync();
		}

		public async Task<bool> UpdateAPIData(APIData apiData)
		{
			try
			{
				_applicationDbContext.Update(apiData);
				await _applicationDbContext.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}

		}
	}
}
