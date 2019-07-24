using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VKApi
{
    public static class DictEngRus
    {
        public static readonly Dictionary<string, string> Dictionary;

		static DictEngRus()
		{
			Dictionary = FillTheDictionary("eng_words.txt");
		}

        public static Dictionary<string, string> FillTheDictionary(string file)
        {
            var dictionary = new Dictionary<string, string>();
			var strArr = File.ReadAllText(file).Split('\n');
			foreach (var str in strArr)
			{
				var keyValue = str.Substring(0, str.Length - 1).Split('&');
				dictionary.Add(keyValue[0], keyValue[1]);
			}

			return dictionary;
        }

        public static string TranslateFromEng(string strKey)
        {
            try
            {
                return Dictionary[strKey];
            }
            catch
            {
                return strKey;
            }
        }
    }
}
