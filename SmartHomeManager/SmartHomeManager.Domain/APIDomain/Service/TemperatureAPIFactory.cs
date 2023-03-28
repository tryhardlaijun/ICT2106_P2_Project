using Newtonsoft.Json;
using SmartHomeManager.Domain.APIDomain.Entities;
using SmartHomeManager.Domain.APIDomain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.APIDomain.Service
{
    public class TemperatureAPIFactory : IAPIFactory
    {
        private readonly IAPIDataRepository _APIDataRepository;

        public TemperatureAPIFactory(IAPIDataRepository _APIDataRepository)
        {
            this._APIDataRepository = _APIDataRepository;
        }

        public IAPICaller CreateAPICaller()
        {
            IAPICaller temperatureCaller = new TemperatureAPICaller(_APIDataRepository);
            return temperatureCaller;
        }

        public IAPIRetriever CreateAPIRetriever()
        {
            IAPIRetriever temperatureRetriever = new TemperatureAPIRetriever(_APIDataRepository);
            return temperatureRetriever;
        }

    }
}
