using SmartHomeManager.Domain.APIDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.APIDomain.Service
{
	public interface IAPIValueRepository
	{
		public Task<IEnumerable<APIValue>> GetAPIValueByKey(String key);
		public Task<IEnumerable<APIValue>> GetAllValue();
		public Task<bool> CreateAPIValue(APIValue apiValue);
	}
}
