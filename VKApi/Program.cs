using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VKApi
{
    class Program
    {
        public static string MyAppToken = "8fa09ca98db869b78d4c7817a84db7ec1a390ebc8464fc1f6be9fbaca0fa4839860d22a2736974a3574ff";
        public static ulong MyGroupId = 177228422;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello");
            var api = new VkApi();
            api.Authorize(new ApiAuthParams() { AccessToken = MyAppToken });

            while (true)
            {
                var s = api.Groups.GetLongPollServer(MyGroupId);
                var poll = api.Groups.GetBotsLongPollHistory(
                                      new BotsLongPollHistoryParams()
                                      { Server = s.Server, Ts = s.Ts, Key = s.Key, Wait = 1 });
                if (poll?.Updates == null) continue;

                foreach (var a in poll.Updates)
                {
                    if (a.Type == GroupUpdateType.MessageNew)
                    {
                        var currentString = a.Message.Body;
                        Console.WriteLine(currentString);
                        var strOut = WeatherAP.GetWeather(currentString);
                        Random random = new Random();
                        api.Messages.Send(new MessagesSendParams()
                        {
                            RandomId = random.Next(0, int.MaxValue),
                            UserId = a.Message.UserId,
                            Message = strOut
                        });
                    }
                }
            }
        }
    }
}
