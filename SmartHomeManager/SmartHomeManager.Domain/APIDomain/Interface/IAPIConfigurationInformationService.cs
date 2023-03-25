using SmartHomeManager.Domain.APIDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.APIDomain.Interface
{
	public interface IAPIConfigurationInformationService
	{
		public Task<IEnumerable<APIKey>> GetAllAPIKey();
		
		public Task<IEnumerable<APIValue>> getAllAPIValue(String APIKey);
	}
}
