using System.Drawing;
using BarcodeLib;
using MailKit.Net.Smtp;
using MimeKit;
using ThjonustukerfiWebAPI.Config.EnvironmentVariables;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Setup;

namespace ThjonustukerfiWebAPI.Services.Implementations
{
    public static class MailService
    {
        /// <summary>Creates a html message to send as an email</summary>
        public static void SendOrderNotification(OrderDTO order, CustomerDetailsDTO customer, double weeksSinceReady)
        {
            var emailAddress = customer.Email;
            var subject = $"{Constants.CompanyName} - Áminning um tilbúna pöntun";
            var body = new BodyBuilder();
            body.HtmlBody = $"<h2>Góðan daginn {order.Customer}.</h2>";
            body.HtmlBody += $"<p>Þú átt ósótta pöntun hjá okkur (nr. {order.Id}) sem kláraðist fyrir {weeksSinceReady} vikum.<br>Pöntun:</p>";
            
            body.HtmlBody += "<ul>";
            foreach (var item in order.Items)
            {
                body.HtmlBody += $"<li> {item.Category} - {item.Service} - staða: {item.State}</li>";
            }
            body.HtmlBody += "</ul><br>";

            // encode to image
            var bCode = new Barcode(); // get barcode lib class
            bCode.Encode(BarcodeLib.TYPE.CODE128, order.Barcode, Color.Black, Color.White, BarcodeImageDimensions.Width, BarcodeImageDimensions.Height);

            var image = body.LinkedResources.Add("Barcode", bCode.Encoded_Image_Bytes);
            image.ContentId = MimeKit.Utils.MimeUtils.GenerateMessageId();
            body.HtmlBody += string.Format(@"<h3>Strikamerki:</h3> <img src=""cid:{0}"">", image.ContentId);

            body.HtmlBody += $"<p>Kær kveðja {Constants.CompanyName}</p>";

            MailService.Sendmail(emailAddress, subject, body);
        }

        /// <summary>Creates a html message to send as an email</summary>
        public static void sendOrderComplete(OrderDTO order, CustomerDetailsDTO customer)
        {
            var emailAddress = customer.Email;
            var subject = $"{Constants.CompanyName} - pöntunin þín er klár.";
            var body = new BodyBuilder();
            body.HtmlBody = $"<h2>Góðan daginn {order.Customer}.</h2>";

            body.HtmlBody += $"<p>Pöntunin þín (nr. {order.Id}) er tilbúin til afhendingar.<br>Pöntun:</p>";
            body.HtmlBody += "<ul>";
            foreach (var item in order.Items)
            {
                body.HtmlBody += $"<li>{item.Category} - {item.Service} - staða: {item.State}</li>";
            }
            body.HtmlBody += "</ul><br>";

            // encode to image
            var bCode = new Barcode(); // get barcode lib class
            bCode.Encode(BarcodeLib.TYPE.CODE128, order.Barcode, Color.Black, Color.White, BarcodeImageDimensions.Width, BarcodeImageDimensions.Height);

            // Send image as attachment and embed it to the message
            var image = body.LinkedResources.Add("Barcode", bCode.Encoded_Image_Bytes);
            image.ContentId = MimeKit.Utils.MimeUtils.GenerateMessageId();
            body.HtmlBody += string.Format(@"<h3>Strikamerki:</h3> <img src=""cid:{0}"">", image.ContentId);

            body.HtmlBody += $"<p>Kær kveðja {Constants.CompanyName}</p>";

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

            MailService.Sendmail(emailAddress, subject, body);
        }

        /// <summary>Sends an email message via SMTP server with credentials from the environment variable file.</summary>
        private static void Sendmail (string emailAddress, string subject, BodyBuilder bodyBuilder)
        {
            var env = EnvironmentFileManager.LoadEvironmentFile();      // load env variables
            string smtpUsername, smtpPassword, smtpServer, smtpPort;    // variables needed to connect and send
            env.TryGetValue("SMTP_USERNAME", out smtpUsername);         // get username
            env.TryGetValue("SMTP_PASSWORD", out smtpPassword);         // get password
            env.TryGetValue("SMTP_SERVER", out smtpServer);             // get server address
            env.TryGetValue("SMTP_PORT", out smtpPort);                 // get server port
            
            // if environmental variables are not set correctly throw exception
            if(smtpUsername == null || smtpPassword == null || smtpServer == null || smtpPort == null)
            {
                throw new EmailException($"Could not send email. Could not find url or authentication key for the email request.");
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Constants.CompanyName, Constants.CompanyEmail));    // Add company email info
            message.To.Add(new MailboxAddress(emailAddress));                                       // Add customer email
            message.Subject = subject;                                                              // Add email subject
            message.Body = bodyBuilder.ToMessageBody();                                             // Constructs the message

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
        }
    }
}