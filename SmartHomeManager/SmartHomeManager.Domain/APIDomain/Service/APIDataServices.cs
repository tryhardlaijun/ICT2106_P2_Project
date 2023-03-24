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

		//creating the keyvalues in the APIKey database
		public async Task createKeyDetails() {

			IEnumerable<APIKey> test = await _APIKeyRepository.GetAllAPIKey();

			if (test.Count() != 0)
			{
				foreach (APIKey testKey in test)
				{
					if (testKey.APIKeyType != "temperature_less_than")
					{
						APIKey TempLessKey = new APIKey
						{
							APIKeyType = "temperature_less_than",
							APILabelText = "Temperature less than"
						};
						await _APIKeyRepository.CreateAPIKey(TempLessKey);
					}
					if (testKey.APIKeyType != "temperature_equals_to")
					{
						APIKey TempSameKey = new APIKey
						{
							APIKeyType = "temperature_equals_to",
							APILabelText = "Temperature Equals to"
						};

						await _APIKeyRepository.CreateAPIKey(TempSameKey);
					}

					if (testKey.APIKeyType != "temperature_more_than")
					{
						APIKey TempMoreKey = new APIKey
						{
							APIKeyType = "temperature_more_than",
							APILabelText = "Temperature More than"
						};

						await _APIKeyRepository.CreateAPIKey(TempMoreKey);
					}
					if (testKey.APIKeyType != "weather")
					{

						APIKey WeatherKey = new APIKey
						{
							APIKeyType = "weather",
							APILabelText = "Weather"
						};

						await _APIKeyRepository.CreateAPIKey(WeatherKey);
					}
				}
			}
			else
			{
				APIKey TempLessKey = new APIKey
				{
					APIKeyType = "temperature_less_than",
					APILabelText = "Temperature less than"
				};
				await _APIKeyRepository.CreateAPIKey(TempLessKey);

				APIKey TempSameKey = new APIKey
				{
					APIKeyType = "temperature_equals_to",
					APILabelText = "Temperature Equals to"
				};

				await _APIKeyRepository.CreateAPIKey(TempSameKey);


				APIKey TempMoreKey = new APIKey
				{
					APIKeyType = "temperature_more_than",
					APILabelText = "Temperature More than"
				};

				await _APIKeyRepository.CreateAPIKey(TempMoreKey);

				APIKey WeatherKey = new APIKey
				{
					APIKeyType = "weather",
					APILabelText = "Weather"
				};

				await _APIKeyRepository.CreateAPIKey(WeatherKey);

			}

		}
		//getting all the Key 
		public async Task<IEnumerable<APIKey>> GetAllAPIKey()
		{
			return await _APIKeyRepository.GetAllAPIKey();
		}

		//Creating the values of the keys 
		public async Task createValuesDetails()
		{
			IEnumerable<APIValue> test = await _APIValueRepository.GetallValue();

			if (test.Count() == 0)
			{
				APIValue TempLessKey = new APIValue
				{
					APIKeyType = "temperature_less_than",
					APIValues = string.Empty
				};
				await _APIValueRepository.CreateAPIValue(TempLessKey);

				APIValue TempSameKey = new APIValue
				{
					APIKeyType = "temperature_equals_to",
					APIValues = string.Empty
				};

				await _APIValueRepository.CreateAPIValue(TempSameKey);


				APIValue TempMoreKey = new APIValue
				{
					APIKeyType = "temperature_more_than",
					APIValues = string.Empty
				};

				await _APIValueRepository.CreateAPIValue(TempMoreKey);

				APIValue SunnyValue = new APIValue
				{
					APIKeyType = "weather",
					APIValues = "Sunny"
				};

				await _APIValueRepository.CreateAPIValue(SunnyValue);

				APIValue ShowersValue = new APIValue
				{
					APIKeyType = "weather",
					APIValues = "Showers"
				};

				await _APIValueRepository.CreateAPIValue(ShowersValue);

				APIValue TShowerValue = new APIValue
				{
					APIKeyType = "weather",
					APIValues = "Thundery Showers"
				};

				await _APIValueRepository.CreateAPIValue(TShowerValue);

				APIValue CloudyValue = new APIValue
				{
					APIKeyType = "weather",
					APIValues = "Cloudy"
				};

				await _APIValueRepository.CreateAPIValue(CloudyValue);

				APIValue PCloudyValue = new APIValue
				{
					APIKeyType = "weather",
					APIValues = "Partly Cloudy"
				};

				await _APIValueRepository.CreateAPIValue(PCloudyValue);

				APIValue LRainValue = new APIValue
				{
					APIKeyType = "weather",
					APIValues = "Light Rain"
				};

				await _APIValueRepository.CreateAPIValue(LRainValue);

				APIValue MRainValue = new APIValue
				{
					APIKeyType = "weather",
					APIValues = "Moderate Rain"
				};

				await _APIValueRepository.CreateAPIValue(MRainValue);


			}


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
			await updateAPIDetails();
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
				if (testItem.Specification.Equals("same"))
				{
					sendKeyValue.Add("weather", testItem.Value);
				}
				
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
