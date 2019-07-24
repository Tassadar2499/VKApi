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
using AlphaLogger;

namespace VKApi
{
    class Program
    {
		//public static LinkedList<Thread> Workers = new LinkedList<Thread>();
		public static Logger Logger = new Logger();
        static void Main()
        {
			Logger.WriteMessage("-------------------------------------------------------", "svc");
			Logger.WriteMessage("Старт сеанса \r\n", "svc");

			var api = new VkApi();
            api.Authorize(new ApiAuthParams() { AccessToken = KeysRepos.MyAppToken });
			Logger.WriteMessage("Авторизовался \r\n", "svc");

			while (true)
            {
                var s = api.Groups.GetLongPollServer(KeysRepos.MyGroupId);
                var poll = api.Groups.GetBotsLongPollHistory(
                                      new BotsLongPollHistoryParams()
                                      { Server = s.Server, Ts = s.Ts, Key = s.Key, Wait = 1 });
                if (poll?.Updates == null) continue;

                foreach (var a in poll.Updates)
                {
					var thread = new Thread(() => MessageHandler.HandleMessage(a, api));
					thread.Start();
					//Workers.AddLast(thread);
				}
            }
        }
    }
}
