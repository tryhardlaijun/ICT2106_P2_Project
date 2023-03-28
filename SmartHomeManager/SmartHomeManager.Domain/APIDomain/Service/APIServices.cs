using SmartHomeManager.Domain.APIDomain.Entities;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartHomeManager.Domain.APIDomain.Interface;
using SmartHomeManager.Domain.SceneDomain.Entities;
using SmartHomeManager.Domain.HomeSecurityDomain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace SmartHomeManager.Domain.APIDomain.Service
{
	public class APIServices : IAPIService, IAPIConfigurationInformationService
	{

		private readonly IAPIDataRepository _APIDataRepository;
		private readonly IAPIKeyRepository _APIKeyRepository;
		private readonly IAPIValueRepository _APIValueRepository;

		private ICollection<IAPIFactory> APIFactories;

		public APIServices(IAPIDataRepository APIDataRepository, IAPIKeyRepository APIKeyRepository, IAPIValueRepository APIValueRepository)
		{
			_APIDataRepository = APIDataRepository;
			_APIKeyRepository = APIKeyRepository;
			_APIValueRepository = APIValueRepository;

            APIFactories = new List<IAPIFactory>();
            APIFactories.Add(new TemperatureAPIFactory(_APIDataRepository));
            APIFactories.Add(new WeatherAPIFactory(_APIDataRepository));
        }

        // For each API type (factory), retrieve and execute the call to get data from web
        public async Task updateAPIData()
        {
            foreach (var fact in APIFactories)
			{
				IAPICaller caller = fact.CreateAPICaller();
				await caller.callAPIFromWeb();
            }
        }

        public async Task<IDictionary<string, string>> getAPIData()
        {
            IDictionary<string, string> combinedApiData = new Dictionary<string, string>();
            foreach (var fact in APIFactories)
            {
                IAPIRetriever retriever = fact.CreateAPIRetriever();
                IDictionary<string, string> apiData = await retriever.getAPIData();
                foreach (var api in apiData)
                {
                    combinedApiData.Add(api);
                }
            }
            
            return combinedApiData;
        }
  
        //getting all the Key 
        public async Task<IEnumerable<APIKey>> GetAllAPIKey()
        {
            return await _APIKeyRepository.GetAllAPIKey();
        }
        //getting all the data 
        public async Task<IEnumerable<APIData>> GetAllAPIData()
        {
            return await _APIDataRepository.GetAllAPIData();
        }

        //getting all the values based on key
        public async Task<IEnumerable<APIValue>> getAllAPIValue(string APIKey)
        {
            return await _APIValueRepository.GetAPIValueByKey(APIKey);
        }

    }
}
