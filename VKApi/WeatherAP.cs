using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VKApi
{
    public static class WeatherAP
    {
        public static string GetWeather(string city)
        {
            var result = "";
            var key = "&appid=03133e2dfe331abba3e6b5912b3fa4a8";
            if (city == "старт") return "Погнали";
            string weburl = "http://api.openweathermap.org/data/2.5/weather?q=" + city + key;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(weburl);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string response;
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    response = streamReader.ReadToEnd();
                result = ConvertCityWeather(response, city);
            }
            catch
            {
                result = "Неверный формат строки";
                Console.WriteLine(result);
            }

            return result;
        }

        private static string ConvertCityWeather(string response, string cityName)
        {
            Func<string[], string, string> findStrByQuery = 
                (string[] strArray, string query) =>
                    strArray.Where(l => l.Contains(query)).ToArray()[0];
         
            var cityWeather = new CityWeather(cityName);
            var strArr = response.Split(',');            
            strArr = strArr.Select(l => l.Replace('"', ' ')).ToArray();
            var strTemperature = findStrByQuery(strArr, "temp");
            var strCloud = findStrByQuery(strArr, "description");
            var strWind = findStrByQuery(strArr, "wind");
            strTemperature = strTemperature.Substring(strTemperature.LastIndexOf(':') + 1);
            strCloud = strCloud.Substring(strCloud.LastIndexOf(':') + 2, strCloud.Length - (strCloud.LastIndexOf(':') + 3));
            strWind = strWind.Substring(strWind.LastIndexOf(':') + 1);
            strWind += " м/с";
            strTemperature = strTemperature.Replace('.', ',');
            var temperature = double.Parse(strTemperature) - 273.15;
            temperature = Math.Round(temperature);
            cityWeather.Cloud = strCloud;
            cityWeather.Wind = strWind;
            cityWeather.Temperature = temperature;
            Console.WriteLine(cityWeather.ToString());

            return cityWeather.ToString();
        }

        private static XmlDocument GetXML(string currentURL)
        {
            using (WebClient client = new WebClient())
            {
                string xmlContent = client.DownloadString(currentURL);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlContent);
                return xmlDocument;
            }
        }
    }
}
