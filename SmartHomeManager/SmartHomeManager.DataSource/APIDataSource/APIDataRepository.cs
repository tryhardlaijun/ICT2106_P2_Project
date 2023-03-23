using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartHomeManager.Domain.APIDomain.Entities;
using SmartHomeManager.Domain.APIDomain.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

		public async Task<List<APIData>> GetAllAPI()
		{
			return await _applicationDbContext.APIDatas.ToListAsync();
		}

		public async Task<List<APIData>> GetAPIDataById(Guid APIDataId)
		{
			return await _applicationDbContext.APIDatas.Where(s => s.APIDataId == APIDataId).ToListAsync();
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
