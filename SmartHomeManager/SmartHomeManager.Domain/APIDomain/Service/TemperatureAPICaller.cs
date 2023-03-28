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
    public class TemperatureAPICaller : IAPICaller
    {
        private readonly IAPIDataRepository _APIDataRepository;

        public TemperatureAPICaller(IAPIDataRepository _APIDataRepository)
        {
            this._APIDataRepository = _APIDataRepository;
        }
        public async Task callAPIFromWeb()
        {
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
                dynamic temperature = day.general.temperature;
                int temp = (temperature.high + temperature.low) / 2;
                IEnumerable<APIData> temperature_data = await _APIDataRepository.GetAPIType("Temperature");

                if (temperature_data.Count() == 0)
                {
                    APIData TempData = new APIData
                    {
                        APIDataId = apiId,
                        Type = "Temperature",
                        Value = temp.ToString(),
                        Specification = "same",
                        TimeStamp = DateTime.Now
                    };
                    await _APIDataRepository.CreateAPIData(TempData);
                }
                else
                {
                    foreach (APIData temperature_item in temperature_data)
                    {

                        if (Int32.Parse(temperature_item.Value) == temp)
                        {
                            temperature_item.Value = temp.ToString();
                            temperature_item.Specification = "same";
                            temperature_item.TimeStamp = DateTime.Now;
                        }
                        else if (Int32.Parse(temperature_item.Value) < temp)
                        {
                            temperature_item.Value = temp.ToString();
                            temperature_item.Specification = "increase";
                            temperature_item.TimeStamp = DateTime.Now;
                        }
                        else if (Int32.Parse(temperature_item.Value) > temp)
                        {
                            temperature_item.Value = temp.ToString();
                            temperature_item.Specification = "decrease";
                            temperature_item.TimeStamp = DateTime.Now;
                        }


                        await _APIDataRepository.UpdateAPIData(temperature_item);

                    }
                }
            }
        }
    }
}
