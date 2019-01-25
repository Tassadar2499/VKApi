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
        public string Wind { get; set; }
        public string Cloud { get; set; }

        public CityWeather(string city)
        {
            City = city;
        }

        public override string ToString()
        {
            return "Город " + City + "\r\n" +
                   "Температура " + Temperature + " градусов" + "\r\n" +
                   "Ветер " + Wind + "\r\n" +
                   Cloud;
        }
    }
}
