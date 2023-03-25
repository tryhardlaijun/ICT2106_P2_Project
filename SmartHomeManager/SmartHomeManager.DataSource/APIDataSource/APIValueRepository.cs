using Microsoft.EntityFrameworkCore;
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

		public async Task<bool> CreateAPIValue(APIValue apiValue)
		{
			try
			{
				await _applicationDbContext.APIValues.AddAsync(apiValue);
				await _applicationDbContext.SaveChangesAsync();
				return true;
			}
			catch { 
				return false;
			}
		}

		public async Task<IEnumerable<APIValue>> GetAPIValueByKey(String key)
		{
			return await _applicationDbContext.APIValues.Where(s => s.APIKey.Equals(key)).ToListAsync();
			
		}

		public async Task<IEnumerable<APIValue>> GetAllValue()
		{
			return await _applicationDbContext.APIValues.ToListAsync();


		}

	}
}
