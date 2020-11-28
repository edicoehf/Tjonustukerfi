using System.Threading.Tasks;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Config.EnvironmentVariables;
using ThjonustukerfiWebAPI.Providers.SMS;

namespace ThjonustukerfiWebAPI.Services.Implementations
{
    public class SMSService
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(SMSService));

        public static async void sendOrderCompleteSMS(OrderDTO order, CustomerDetailsDTO customer)
        {
            try
            {
                await Task.Run(() => 
                {
                   var env = EnvironmentFileManager.LoadEvironmentFile();
                    string smsUsername, smsPassword, smsUrl, smsBackend;
                    env.TryGetValue("SMS_USER",    out smsUsername);
                    env.TryGetValue("SMS_PASS",    out smsPassword);
                    env.TryGetValue("SMS_URL",     out smsUrl);
                    env.TryGetValue("SMS_BACKEND", out smsBackend);
 
                    // We only need the Siminn Provider for now, but later we might bring others into play.
                    var smsProvider = new SMSSiminnServiceProvider();
                    smsProvider.Username  = smsUsername;
                    smsProvider.Password  = smsPassword;
                    smsProvider.ServerURL = smsUrl;
                    smsProvider.From      = "Reykofninn";

                    var message = "Pöntun " + order.Id + " er tilbúin. Kveðja, Reykofninn";

                    _log.Info($"About to send SMS to {customer.Phone}. Config is user={smsUsername}, pass={smsPassword}, url={smsUrl}");

                    smsProvider.SendSMS(message, customer.Phone);

               }); 
            }
            catch (System.Exception ex)
            {
                _log.Error(new EmailException($"Could not send SMS.\nMessage from the caught exception:\n{ex.Message}\nStackTrace:\n{ex.StackTrace}"));
            }
             
        }
    }
}