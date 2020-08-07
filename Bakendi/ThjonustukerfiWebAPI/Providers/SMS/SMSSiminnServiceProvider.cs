using System;
using log4net;
using RestSharp;

namespace ThjonustukerfiWebAPI.Providers.SMS
{
	public class SMSSiminnServiceProvider : SMSServiceProvider
	{
		private static readonly ILog log = LogManager.GetLogger(typeof(SMSSiminnServiceProvider));

		public string Username  { get; set; }
		public string Password  { get; set; }
		public string From      { get; set; }
		public string ServerURL { get; set; }

		public void SendSMS(string message, string phoneNumber)
		{
			var client = new RestClient(ServerURL);
			var msgConverted = SMSUtils.ReplaceIcelandicLetters(message);

			phoneNumber = phoneNumber.Replace("-", "").Replace(" ", "");

			var request = new RestRequest("/smap/push", Method.POST);
			request.AddParameter("L", Username);
			request.AddParameter("P", Password);
			request.AddParameter("msisdn", phoneNumber);
			request.AddParameter("T", msgConverted);

			try
			{
				client.ExecuteAsync(request, response =>
				{
					log.Info("SMS Service response: " + response.Content);
				});

				//var task = client.ExecuteTaskAsync(request);
				//task.Wait();

			}
			catch (Exception exception)
			{
				log.Error("Error while sending SMS message: " + exception.Message, exception);
			}
		}
	}
}
