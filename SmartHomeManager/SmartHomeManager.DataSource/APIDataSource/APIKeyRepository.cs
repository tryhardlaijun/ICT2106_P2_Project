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
	public class APIKeyRepository : IAPIKeyRepository
	{
		private readonly ApplicationDbContext _applicationDbContext;
		public APIKeyRepository(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public async Task<bool> CreateAPIKey(APIKey apiKey)
		{

			try
			{
				await _applicationDbContext.APIKeys.AddAsync(apiKey);
				await _applicationDbContext.SaveChangesAsync();
				return true;
			}
			catch { 
				return false;
			}

		}

		public async Task<IEnumerable<APIKey>> GetAllAPIKey()
		{
			return await _applicationDbContext.APIKeys.ToListAsync();
		}


	}
}
