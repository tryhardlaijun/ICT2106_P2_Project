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
    public class WeatherAPICaller : IAPICaller
    {
        private readonly IAPIDataRepository _APIDataRepository;

        public WeatherAPICaller(IAPIDataRepository _APIDataRepository)
        {
            this._APIDataRepository = _APIDataRepository;
        }
        public async Task callAPIFromWeb()
        {
            Console.WriteLine("help");
            Guid apiId = Guid.NewGuid();
            String now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            now = now.Replace(" ", "T");
            now = now.Replace(":", "%3A");
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.data.gov.sg/v1/environment/24-hour-weather-forecast?date_time=" + now);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            dynamic jsonBody = JsonConvert.DeserializeObject(body);

            foreach (var day in jsonBody.items)
            {
                var weather = day.general;
                var forecast = weather.forecast;
                IEnumerable<APIData> weather_data = await _APIDataRepository.GetAPIType("Weather");
                if (weather_data.Count() == 0)
                {
                    APIData WeatherData = new APIData
                    {
                        APIDataId = apiId,
                        Type = "Weather",
                        Value = forecast,
                        Specification = "same",
                        TimeStamp = DateTime.Now
                    };
                    await _APIDataRepository.CreateAPIData(WeatherData);
                }
                else
                {
                    var weather_item = weather_data.First();
                    
                    if (!weather_item.Value.Equals(forecast))
                    {
                        weather_item.Value = forecast;
                        weather_item.Specification = "changed";
                        weather_item.TimeStamp = DateTime.Now;
                    }
                    else
                    {
                        weather_item.Value = forecast;
                        weather_item.Specification = "same";
                        weather_item.TimeStamp = DateTime.Now;
                    }
                    await _APIDataRepository.UpdateAPIData(weather_item);
                }
            }
        }
    }
}
