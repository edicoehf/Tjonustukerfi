using System.Drawing;
using System.Threading.Tasks;
using BarcodeLib;
using MailKit.Net.Smtp;
using MimeKit;
using ThjonustukerfiWebAPI.Config.EnvironmentVariables;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Services.Interfaces;
using ThjonustukerfiWebAPI.Setup;

namespace ThjonustukerfiWebAPI.Services.Implementations
{
    public static class MailService
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(MailService));

        /// <summary>Creates a html message to send as an email</summary>
        public static void SendOrderNotification(OrderDTO order, CustomerDetailsDTO customer, double weeksSinceReady)
        {
            var emailAddress = customer.Email;
            var subject      = $"{Constants.CompanyName} - Áminning um tilbúna pöntun";
            var body         = new BodyBuilder();
            body.HtmlBody    = $"<h2>Góðan daginn {order.Customer}.</h2>";
            body.HtmlBody    += $"<p>Þú átt ósótta pöntun hjá okkur (nr. {order.Id}) sem kláraðist fyrir {weeksSinceReady} vikum.<br>Pöntun:</p>";
            
            body.HtmlBody += "<ul>";
            foreach (var item in order.Items)
            {
                body.HtmlBody += $"<li> {item.Category} - {item.Service}, magn: {item.Quantity} - staða: {item.State}</li>";
            }
            body.HtmlBody += "</ul><br>";

            // encode to image
            var bCode = new Barcode(); // get barcode lib class
            bCode.Encode(BarcodeLib.TYPE.CODE128, order.Barcode, Color.Black, Color.White, BarcodeImageDimensions.Width, BarcodeImageDimensions.Height);

            var image = body.LinkedResources.Add("Barcode", bCode.Encoded_Image_Bytes);
            image.ContentId = MimeKit.Utils.MimeUtils.GenerateMessageId();
            body.HtmlBody += string.Format(@"<h3>Strikamerki:</h3> <img src=""cid:{0}"">", image.ContentId);

            if (!string.IsNullOrEmpty(Constants.CompanyEmailDisclaimer))
            {
                body.HtmlBody += "<br>";
                body.HtmlBody += Constants.CompanyEmailDisclaimer;
            }

            body.HtmlBody += $"<p>Kær kveðja {Constants.CompanyName}</p>";
            body.HtmlBody += "Opnunartími:  Mán - Fös frá 9 til 17.";

            MailService.Sendmail(emailAddress, subject, body, Constants.CompanyCc);
        }

        public static void sendOrderReceived(OrderDTO order, CustomerDetailsDTO customer)
        {
            var emailAddress = customer.Email;
            if (!string.IsNullOrEmpty(emailAddress))
            {
                var subject = $"{Constants.CompanyName} - pöntun móttekin";
                var body = new BodyBuilder();
                body.HtmlBody = $"<h2>Góðan daginn {customer.Name}.</h2>";

                body.HtmlBody += $"<p>Pöntunin þín (nr. {order.Id}) er móttekin.<br>Pöntun:</p>";
                body.HtmlBody += "<ul>";
                foreach (var item in order.Items)
                {
                    body.HtmlBody += $"<li>{item.Category} - {item.Service}, magn: {item.Quantity}</li>";
                }
                body.HtmlBody += "</ul><br>";

                // encode to image
                var bCode = new Barcode(); // get barcode lib class
                bCode.Encode(BarcodeLib.TYPE.CODE128, order.Barcode, Color.Black, Color.White, BarcodeImageDimensions.Width, BarcodeImageDimensions.Height);

                // Send image as attachment and embed it to the message
                var image = body.LinkedResources.Add("Barcode", bCode.Encoded_Image_Bytes);
                image.ContentId = MimeKit.Utils.MimeUtils.GenerateMessageId();
                body.HtmlBody += string.Format(@"<h3>Strikamerki:</h3> <img src=""cid:{0}"">", image.ContentId);

                if (!string.IsNullOrEmpty(Constants.CompanyEmailDisclaimer))
                {
                    body.HtmlBody += "<br>";
                    body.HtmlBody += Constants.CompanyEmailDisclaimer;
                }

                body.HtmlBody += $"<p>Kær kveðja {Constants.CompanyName}</p>";
                body.HtmlBody += "Opnunartími:  Mán - Fös frá 9 til 17.";

                MailService.Sendmail(emailAddress, subject, body);
            }
        }

        /// <summary>Creates a html message to send as an email</summary>
        public static void sendOrderComplete(OrderDTO order, CustomerDetailsDTO customer)
        {
            var emailAddress = customer.Email;
            var subject = $"{Constants.CompanyName} - pöntunin þín er klár";
            var body = new BodyBuilder();
            body.HtmlBody = $"<h2>Góðan daginn {order.Customer}.</h2>";

            body.HtmlBody += $"<p>Pöntunin þín (nr. {order.Id}) er tilbúin til afhendingar.<br>Pöntun:</p>";
            body.HtmlBody += "<ul>";
            foreach (var item in order.Items)
            {
                body.HtmlBody += $"<li>{item.Category} - {item.Service}, magn: {item.Quantity} - staða: {item.State}</li>";
            }
            body.HtmlBody += "</ul><br>";

            // encode to image
            var bCode = new Barcode(); // get barcode lib class
            bCode.Encode(BarcodeLib.TYPE.CODE128, order.Barcode, Color.Black, Color.White, BarcodeImageDimensions.Width, BarcodeImageDimensions.Height);

            // Send image as attachment and embed it to the message
            var image = body.LinkedResources.Add("Barcode", bCode.Encoded_Image_Bytes);
            image.ContentId = MimeKit.Utils.MimeUtils.GenerateMessageId();
            body.HtmlBody += string.Format(@"<h3>Strikamerki:</h3> <img src=""cid:{0}"">", image.ContentId);

            if (!string.IsNullOrEmpty(Constants.CompanyEmailDisclaimer))
            {
                body.HtmlBody += "<br>";
                body.HtmlBody += Constants.CompanyEmailDisclaimer;
            }

            body.HtmlBody += $"<p>Kær kveðja {Constants.CompanyName}</p>";
            body.HtmlBody += "Opnunartími:  Mán - Fös frá 9 til 17.";

            MailService.Sendmail(emailAddress, subject, body);
        }

        public static void sendBarcodeEmail(OrderDTO order, CustomerDetailsDTO customer)
        {
            var emailAddress = customer.Email;
            var subject = $"{Constants.CompanyName} - upplýsingar";
            var body = new BodyBuilder();
            body.HtmlBody = $"<h2>Góðan daginn {order.Customer}.</h2>";

            body.HtmlBody += $"<p>Pöntunarnúmerið þitt er (nr. {order.Id})</p>";
            
            body.HtmlBody += "<h3>Strikamerki vöru:</h3>";
            foreach (var item in order.Items)
            {
                // encode to image
                var bCode = new Barcode(); // get barcode lib class
                bCode.Encode(BarcodeLib.TYPE.CODE128, item.Barcode, Color.Black, Color.White, BarcodeImageDimensions.Width, BarcodeImageDimensions.Height);

                // Send image as attachment and embed it to the message
                var image = body.LinkedResources.Add("Barcode", bCode.Encoded_Image_Bytes);
                image.ContentId = MimeKit.Utils.MimeUtils.GenerateMessageId();
                body.HtmlBody += string.Format(@"<img src=""cid:{0}"">", image.ContentId);
                body.HtmlBody += "<br>";
            }

            body.HtmlBody += $"<p>Kær kveðja {Constants.CompanyName}</p>";
            body.HtmlBody += "Opnunartími:  Mán - Fös frá 9 til 17.";

            MailService.Sendmail(emailAddress, subject, body);
        }

        /// <summary>Sends an email message via SMTP server with credentials from the environment variable file.</summary>
        private static async void Sendmail (string emailAddress, string subject, BodyBuilder bodyBuilder, string cc = null)
        {
            try
            {
                await Task.Run(() => 
                {
                    var env = EnvironmentFileManager.LoadEvironmentFile();      // load env variables
                    string smtpUsername, smtpPassword, smtpServer, smtpPort;    // variables needed to connect and send
                    env.TryGetValue("SMTP_USERNAME", out smtpUsername);         // get username
                    env.TryGetValue("SMTP_PASSWORD", out smtpPassword);         // get password
                    env.TryGetValue("SMTP_SERVER", out smtpServer);             // get server address
                    env.TryGetValue("SMTP_PORT", out smtpPort);                 // get server port

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress(Constants.CompanyName, Constants.CompanyEmail));    // Add company email info
                    message.To.Add(new MailboxAddress(emailAddress));                                       // Add customer email
                    message.Subject = subject;                                                              // Add email subject
                    message.Body = bodyBuilder.ToMessageBody();                                             // Constructs the message

                    if (!string.IsNullOrEmpty(cc))
                    {
                        message.Cc.Add(new MailboxAddress(cc));
                    }

                    //_log.Info("About to send email to:" + emailAddress);
                    //_log.Info(message.Body);

                    using (var client = new SmtpClient())   // Mailkit SmtpClient
                    {
                        // connect  Note:   mailkit has a connect function with the 3rd parameter as useSSL that you can set to false. This is
                        //                  not enough when connecting to some servers, this method will make sure their is no secure socket connection (if needed)
                        client.Connect(smtpServer, int.Parse(smtpPort), MailKit.Security.SecureSocketOptions.None);

                        // authenticate
                        client.Authenticate(smtpUsername, smtpPassword);

                        // send and disconnect
                        client.Send(message);
                        client.Disconnect(true);
                    }
                }); 
            }
            catch (System.Exception ex)
            {
                _log.Error(new EmailException($"Could not send email.\nMessage from the caught exception:\n{ex.Message}\nStackTrace:\n{ex.StackTrace}"));
            }
        }
    }
}