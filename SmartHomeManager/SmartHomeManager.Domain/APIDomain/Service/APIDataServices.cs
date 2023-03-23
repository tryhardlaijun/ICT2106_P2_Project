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

namespace SmartHomeManager.Domain.APIDomain.Service
{
	public class APIDataServices : IAPIDataService
	{

		private readonly IAPIDataRepository _APIDataRepository;


		public APIDataServices(IAPIDataRepository APIDataRepository)
		{
			_APIDataRepository = APIDataRepository;

		}

		public async Task getAPIData()
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

			dynamic weather = JsonConvert.DeserializeObject(body);

			foreach (var day in weather.items)
			{
				dynamic weather_1 = day.general;
				dynamic temperature = day.general.temperature;

				int temp = temperature.high;
				var test = await _APIDataRepository.GetAllAPI();
				var spec = "";
				if (test.Count() == 0)
				{
					spec = "same";

				}
				else
				{
					if (test[0].Type == "Temperature")
					{
						if (int.Parse(test[0].Value) == int.Parse(temperature.high))
						{
							spec = "same";
						}
						else if (int.Parse(test[0].Value) < int.Parse(temperature.high))
						{
							spec = "increase";
						}
						else if (int.Parse(test[0].Value) > int.Parse(temperature.high))
						{
							spec = "decrease";
						}
					}

				}


				APIData TempData = new APIData
				{
					APIDataId = apiId,
					Type = "Temperature",
					Value = temp.ToString(),
					Specification = spec,
					TimeStamp = DateTime.Now
				};

				await _APIDataRepository.CreateAPIData(TempData);

			}



		}


		//write the create api here!
		//getAPIData == createApiData
	}
}
