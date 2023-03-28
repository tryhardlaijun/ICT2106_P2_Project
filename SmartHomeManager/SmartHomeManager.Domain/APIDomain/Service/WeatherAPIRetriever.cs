using SmartHomeManager.Domain.APIDomain.Entities;
using SmartHomeManager.Domain.APIDomain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.APIDomain.Service
{
    public class WeatherAPIRetriever : IAPIRetriever
    {
        private readonly IAPIDataRepository _APIDataRepository;

        public WeatherAPIRetriever(IAPIDataRepository _APIDataRepository)
        {
            this._APIDataRepository = _APIDataRepository;
        }
        public async Task<IDictionary<string, string>> getAPIData()
        {
            IDictionary<string, string> keyValue = new Dictionary<string, string>();
            IEnumerable<APIData> weather = await _APIDataRepository.GetAPIType("Weather");
            foreach (var weather_item in weather)
            {
                if (weather_item.Specification.Equals("changed"))
                {
                    keyValue.Add("weather", weather_item.Value);
                }
            }
            return keyValue;
        }
    }
}
