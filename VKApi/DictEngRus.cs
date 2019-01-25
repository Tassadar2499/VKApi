using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKApi
{
    public static class DictEngRus
    {
        private static Dictionary<string, string> dictionary;

        public static void FillTheDictionary()
        {
            dictionary = new Dictionary<string, string>();
            dictionary.Add("mist", "туман");
            dictionary.Add("scattered clouds", "рассеянные облака");
            dictionary.Add("broken clouds", "облачно");
            dictionary.Add("clear sky", "ясно");
            dictionary.Add("few clouds", "низкая облачность");
            dictionary.Add("overcast clouds", "пасмурно");
            dictionary.Add("light snow", "легкий снег");
            dictionary.Add("drizzle rain", "моросящий дождь");
            dictionary.Add("snow", "снег");
        }

        public static string TranslateFromEng(string strKey)
        {
            var result = "";
            try
            {
                result = dictionary[strKey];
            }
            catch
            {
                result = strKey;
            }

            return result;
        }
    }
}
