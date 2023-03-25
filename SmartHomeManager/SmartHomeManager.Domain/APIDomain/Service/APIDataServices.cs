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
	public class APIDataServices : IAPIDataService, IAPIConfigurationInformationService
	{

		private readonly IAPIDataRepository _APIDataRepository;
		private readonly IAPIKeyRepository _APIKeyRepository;
		private readonly IAPIValueRepository _APIValueRepository;


		public APIDataServices(IAPIDataRepository APIDataRepository, IAPIKeyRepository APIKeyRepository, IAPIValueRepository APIValueRepository)
		{
			_APIDataRepository = APIDataRepository;
			_APIKeyRepository = APIKeyRepository;
			_APIValueRepository = APIValueRepository;
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

		//Sending dictionary to director for rule checking
		public async Task<IDictionary<string, string>> getAPIData()
		{
			IDictionary<string, string> sendKeyValue = new Dictionary<string, string>();
			//await updateAPIDetails();
			IEnumerable<APIData> test = await _APIDataRepository.GetAPIType("Temperature");
			foreach (APIData testItem in test)
			{
				if (testItem.Specification.Equals("decrease")){
					sendKeyValue.Add("temperature_less_than", testItem.Value);
				}
				if (testItem.Specification.Equals("increase"))
				{
					sendKeyValue.Add("temperature_more_than", testItem.Value);
				}
				if (testItem.Specification.Equals("same"))
				{
					sendKeyValue.Add("temperature_equal_to", testItem.Value);
				}
			}
			IEnumerable<APIData> weather_data = await _APIDataRepository.GetAPIType("Weather");
			foreach (APIData testItem in weather_data)
			{
				if (testItem.Specification.Equals("changed"))
				{
					sendKeyValue.Add("weather", testItem.Value);
				}
				//if (testItem.Specification.Equals("same"))
				//{
				//	sendKeyValue.Add("weather", testItem.Value);
				//}
				
			}
			return sendKeyValue;


		}
		//udpating the table with either a new entry if temperature/weather is missing or update the value
		public async Task updateAPIDetails()
		{
			Guid apiId = Guid.NewGuid();
			Guid apiId2 = Guid.NewGuid();
			String now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			now = now.Replace(" ", "T");
			now = now.Replace(":", "%3A");
			var client = new HttpClient();
			var request = new HttpRequestMessage(HttpMethod.Get, "https://api.data.gov.sg/v1/environment/24-hour-weather-forecast?date_time=" + now);
			var response = await client.SendAsync(request);
			response.EnsureSuccessStatusCode();

			var body = await response.Content.ReadAsStringAsync();

			dynamic weather = JsonConvert.DeserializeObject(body);
				//JsonConvert.DeserializeObject(body);

			foreach (var day in weather.items)
			{
				dynamic weather_1 = day.general;
				dynamic temperature = day.general.temperature;

				int temp = temperature.high;
				IEnumerable<APIData> temp_data = await _APIDataRepository.GetAPIType("Temperature");
				IEnumerable<APIData> weather_data = await _APIDataRepository.GetAPIType("Weather");
				var spec = "";
				if (temp_data.Count() == 0)
				{
					spec = "same";
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
				if (weather_data.Count() == 0)
				{
					spec = "same";
					APIData WeatherData = new APIData
					{
						APIDataId = apiId2,
						Type = "Weather",
						Value = weather_1.forecast,
						Specification = spec,
						TimeStamp = DateTime.Now
					};
					await _APIDataRepository.CreateAPIData(WeatherData);
				}
				else 
				{
					foreach (APIData temp_item in temp_data) {
						
						if (Int32.Parse(temp_item.Value) == temp)
						{
							temp_item.Value = temp.ToString();
							temp_item.Specification = "same";
							temp_item.TimeStamp = DateTime.Now;
						}
						else if (Int32.Parse(temp_item.Value) < temp)
						{
							temp_item.Value = temp.ToString();
							temp_item.Specification = "increase";
							temp_item.TimeStamp = DateTime.Now;
						}
						else if (Int32.Parse(temp_item.Value) > temp)
						{
							temp_item.Value = temp.ToString();
							temp_item.Specification = "decrease";
							temp_item.TimeStamp = DateTime.Now;
						}
						
						
					await _APIDataRepository.UpdateAPIData(temp_item);

					}
					string forecast = weather_1.forecast;

					foreach (APIData weather_item in weather_data) {
						if (weather_item.Value != forecast)
						{
							weather_item.Value = weather_1.forecast;
							weather_item.Specification = "changed";
							weather_item.TimeStamp = DateTime.Now;
						}
						else {
							weather_item.Value = weather_1.forecast;
							weather_item.Specification = "same";
							weather_item.TimeStamp = DateTime.Now;
						}
						await _APIDataRepository.UpdateAPIData(weather_item);
					}
					
				}

			}



		}

	}
}
