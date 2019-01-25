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
            //var city = "London";
            var key = "&appid=03133e2dfe331abba3e6b5912b3fa4a8";
            string weburl = "http://api.openweathermap.org/data/2.5/weather?q=" + city + key;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(weburl);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                response = streamReader.ReadToEnd();
            result = ConvertCityWeather(response, city);

            return result;
        }

        private static string ConvertCityWeather(string response, string cityName)
        {
            var cityWeather = new CityWeather(cityName);
            var strArr = response.Split(',');
            var strTemperature = strArr[7];
            var strCloud = strArr[4];
            var strWind = strArr[13];
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
