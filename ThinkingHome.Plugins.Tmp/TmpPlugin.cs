﻿using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.AlarmClock;
using ThinkingHome.Plugins.Audio;
using ThinkingHome.Plugins.Audio.Internal;
using ThinkingHome.Plugins.Scripts;

namespace ThinkingHome.Plugins.Tmp
{
	[Plugin]
	public class TmpPlugin : PluginBase
	{
		#region camera
		[ScriptCommand("sendDCS930Image")]
		public void SendImage(string to, string subject)
		{
			var cameraIp = ConfigurationManager.AppSettings["testCameraIP"];
			var bytes = GetImage(cameraIp, "admin", "");

			var fileName = string.Format("{0:yyyyMMddHHmmss}.jpg", DateTime.Now);
			var path = Path.Combine(@"D:\", fileName);
			File.WriteAllBytes(path, bytes);

			Send(to, subject, fileName, bytes);
		}

		protected static byte[] GetImage(string ip, string login, string password)
		{
			string url = string.Format(@"http://{0}/image/jpeg.cgi", ip);
			WebRequest request = WebRequest.Create(url);
			request.Credentials = new NetworkCredential(login, password);
			request.PreAuthenticate = true;

			using (var response = request.GetResponse())
			{
				using (var stream = response.GetResponseStream())
				{
					if (stream == null)
					{
						return new byte[0];
					}

					var ms = new MemoryStream();
					stream.CopyTo(ms);
					byte[] data = ms.ToArray();
					return data;
				}
			}
		}

		#endregion

		#region mail

		public void Send(string to, string subject, string fileName, byte[] bytes)
		{
			var message = new MailMessage
			{
				To = { to },
				Subject = subject
			};

			if (bytes != null)
			{
				var attachment = new Attachment(new MemoryStream(bytes), fileName);
				message.Attachments.Add(attachment);
			}

			ThreadPool.QueueUserWorkItem(state =>
			{
				try
				{
					SendEmailAwaitable(message);
				}
				catch (Exception ex)
				{
					Logger.ErrorException("mail exception", ex);
				}
			});
		}

		private void SendEmailAwaitable(MailMessage message)
		{
			using (var smtpClient = new SmtpClient())
			{
				smtpClient.Send(message);
			}
		}

		#endregion

		#region sounds

		private IPlayback stream;
		private readonly object lockObjectForSound = new object();

		[ScriptCommand("playDoorBell")]
		public void DoorBell()
		{
			lock (lockObjectForSound)
			{
				stream = Context.GetPlugin<AudioPlugin>().Play(TmpResources.doorbell, 3);
			}
		}

		[ScriptCommand("stopDoorBell")]
		public void StopDoorBell()
		{
			if (stream != null)
			{
				lock (lockObjectForSound)
				{
					if (stream != null)
					{
						stream.Stop();
						stream = null;
					}
				}
			}
		}

		#endregion

		#region test

		[OnAlarmStarted]
		public void Asfafasfasg1(Guid id)
		{
			Logger.Info("test1: {0}", id);
		}

		[OnAlarmStarted]
		public void Asfafasfasg2(Guid id)
		{
			Logger.Info("test2: {0}", id);
		}



		#endregion
	}
}
