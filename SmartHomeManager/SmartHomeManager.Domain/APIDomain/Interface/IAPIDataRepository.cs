using SmartHomeManager.Domain.APIDomain.Entities;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.APIDomain.Service
{
	
	public interface IAPIDataRepository {
		
		public Task<List<APIData>> GetAPIDataById(Guid APIDataId);
		public Task<IEnumerable<APIData>> GetAPIType(String Type);
		public Task<bool> CreateAPIData(APIData apiData);
		public Task<bool> UpdateAPIData(APIData apiData);

	}
	
}
