using SmartHomeManager.Domain.APIDomain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.APIDomain.Service
{
    public class WeatherAPIFactory : IAPIFactory
    {
        private readonly IAPIDataRepository _APIDataRepository;

        public WeatherAPIFactory(IAPIDataRepository _APIDataRepository)
        {
            this._APIDataRepository = _APIDataRepository;
        }

        public IAPICaller CreateAPICaller()
        {
            IAPICaller weatherCaller = new WeatherAPICaller(_APIDataRepository);
            return weatherCaller;
        }

        public IAPIRetriever CreateAPIRetriever()
        {
            IAPIRetriever weatherRetriever = new WeatherAPIRetriever(_APIDataRepository);
            return weatherRetriever;
        }
    }
}
