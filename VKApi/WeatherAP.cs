using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace VKApi
{
    public static class WeatherAP
    {
        public static string GetWeather(string city)
        {
			if (city == "старт") return "Погнали";
			string weburl = KeysRepos.OpenWeatherURL + city + KeysRepos.OpenWeatherKey;
			string result;
			try
			{
				var response = "";
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(weburl);
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
					response = streamReader.ReadToEnd();
				//Console.WriteLine("Подключился к серверу погоды\r\n");
				Program.Logger.WriteMessage("Подключился к серверу погоды\r\n", "svc");
				var jDictionary = DeserializeToDict(response);
				result = ConvertCityWeather(city, jDictionary);
			}
			catch
			{
				result = "Неверный формат строки";
			}

			Program.Logger.WriteMessage(result, "svc");
			//Console.WriteLine(result + "\r\n");

            return result;
        }

        private static string ConvertCityWeather(string cityName, Dictionary<object, object> jDictionary)
        {

            var cityWeather = new CityWeather(cityName);

            var jMain = jDictionary["main"].ToString();
            var jWind = jDictionary["wind"].ToString();
            var jWeather = jDictionary["weather"].ToString();
            jWeather = jWeather.Trim('[', ']');

            var windDict = DeserializeToDict(jWind);
            var mainDict = DeserializeToDict(jMain);
            var weatherDict = DeserializeToDict(jWeather);

            cityWeather.Cloud = weatherDict["description"].ToString();
            cityWeather.Wind = double.Parse(windDict["speed"].ToString());
            cityWeather.Humidity = int.Parse(mainDict["humidity"].ToString());
            cityWeather.Temperature = Math.Round(double.Parse(mainDict["temp"].ToString()) - 273.15);

            return cityWeather.ToString();
        }

        private static Dictionary<object, object> DeserializeToDict(string jStr)
        {
            return JsonConvert.DeserializeObject<Dictionary<object, object>>(jStr);
        }
    }
}
