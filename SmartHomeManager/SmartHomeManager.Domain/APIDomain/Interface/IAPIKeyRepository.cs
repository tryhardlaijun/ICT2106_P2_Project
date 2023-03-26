using SmartHomeManager.Domain.APIDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.APIDomain.Service
{
	public interface IAPIKeyRepository {
		public Task<IEnumerable<APIKey>> GetAllAPIKey();
		
	}
}
