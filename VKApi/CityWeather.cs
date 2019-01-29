using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKApi
{
    public class CityWeather
    {
        public string City { get; set; }
        public double Temperature { get; set; }
        public double Wind { get; set; }
        public int Humidity { get; set; }
        public string Cloud { get; set; }

        public CityWeather(string city)
        {
            City = city;
        }

        public override string ToString()
        {
            return "Город " + City + "\r\n" +
                   "Температура " + Temperature + " градусов" + "\r\n" +
                   "Ветер " + Wind + " м/с" + "\r\n" +
                   "Влажность " + Humidity + " %" + "\r\n" +
                   DictEngRus.TranslateFromEng(Cloud);
        }
    }
}
