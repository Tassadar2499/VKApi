using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VKApi
{
    class Program
    {
		public static LinkedList<Thread> workers = new LinkedList<Thread>();
        static void Main()
        {
            Console.WriteLine("Старт сеанса \r\n");
            var api = new VkApi();
            api.Authorize(new ApiAuthParams() { AccessToken = KeysRepos.MyAppToken });
            Console.WriteLine("Авторизовался\r\n");

            while (true)
            {
                var s = api.Groups.GetLongPollServer(KeysRepos.MyGroupId);
                var poll = api.Groups.GetBotsLongPollHistory(
                                      new BotsLongPollHistoryParams()
                                      { Server = s.Server, Ts = s.Ts, Key = s.Key, Wait = 1 });
                if (poll?.Updates == null) continue;

                foreach (var a in poll.Updates)
                {
					workers.AddLast(new Thread(() => MessageHandler.HandleMessage(a, api)));
					workers.Last.Value.Start();
				}
            }
        }
    }
}
