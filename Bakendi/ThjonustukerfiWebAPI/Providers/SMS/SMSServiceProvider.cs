namespace ThjonustukerfiWebAPI.Providers.SMS
{
	interface SMSServiceProvider
	{
		string Username  { get; set; }
		string Password  { get; set; }
		string From      { get; set; }
		string ServerURL { get; set; }

		void SendSMS(string message, string phoneNumber);
	}
}
