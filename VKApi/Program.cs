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
        static void Main(string[] args)
        {
            DictEngRus.FillTheDictionary();
            Console.WriteLine("Старт сеанса ");
            Console.WriteLine();
            var api = new VkApi();
            api.Authorize(new ApiAuthParams() { AccessToken = KeysRepos.MyAppToken });
            Console.WriteLine("Авторизовался");
            Console.WriteLine();

            while (true)
            {
                var s = api.Groups.GetLongPollServer(KeysRepos.MyGroupId);
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
                        Console.WriteLine();
                        var strOut = WeatherAP.GetWeather(currentString);
                        var idUser = a.Message.UserId;
                        var randomNum = new Random();
                        api.Messages.Send(new MessagesSendParams()
                        {
                            RandomId = randomNum.Next(0, int.MaxValue),
                            UserId = idUser,
                            Message = strOut
                        });

                        api.Messages.Send(new MessagesSendParams()
                        {
                            RandomId = randomNum.Next(1, int.MaxValue) - 1,
                            UserId = idUser,
                            Message = "Введите название города"
                        });
                    }
                }
            }
        }
    }
}
