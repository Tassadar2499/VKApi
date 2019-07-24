using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Model.GroupUpdate;

namespace VKApi
{
	public static class MessageHandler
	{
		public static void HandleMessage( GroupUpdate a, VkApi api)
		{
			if (a.Type == GroupUpdateType.MessageNew)
			{
				var currentString = a.Message.Body;
				Console.WriteLine(currentString + "\r\n");

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
