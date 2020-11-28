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

			//var request = new RestRequest("/smap/push", Method.POST);
			var request = new RestRequest("/smap/push", Method.GET);
			request.AddParameter("L",      Username,     ParameterType.GetOrPost);
			request.AddParameter("P",      Password,     ParameterType.QueryStringWithoutEncode);
			request.AddParameter("msisdn", phoneNumber,  ParameterType.GetOrPost);
			request.AddParameter("T",      msgConverted, ParameterType.GetOrPost);

			try
			{
				var fullUrl = client.BuildUri(request);
				log.Info($"SMS: about to send request for URI {fullUrl}");

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
