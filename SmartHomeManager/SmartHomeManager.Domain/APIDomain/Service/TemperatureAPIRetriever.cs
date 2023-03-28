using SmartHomeManager.Domain.APIDomain.Entities;
using SmartHomeManager.Domain.APIDomain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SmartHomeManager.Domain.APIDomain.Service
{
    
    public class TemperatureAPIRetriever : IAPIRetriever
    {
        private readonly IAPIDataRepository _APIDataRepository;
        
        public TemperatureAPIRetriever(IAPIDataRepository _APIDataRepository)
        {
            this._APIDataRepository = _APIDataRepository;
        }

        public async Task<IDictionary<string, string>> getAPIData()
        {
            IDictionary<string, string> keyValue = new Dictionary<string, string>();
            IEnumerable<APIData> temperature = await _APIDataRepository.GetAPIType("Temperature");
            foreach (var temperature_item in temperature)
            {
                switch (temperature_item.Specification)
                {
                    case "decrease":
                        keyValue.Add("temperature_less_than", temperature_item.Value);
                        break;
                    case "increase":
                        keyValue.Add("temperature_more_than", temperature_item.Value);
                        break;
                    case "same":
                        keyValue.Add("temperature_more_than", temperature_item.Value);
                        break;
                }
            }
            return keyValue;
        }



    }
}
