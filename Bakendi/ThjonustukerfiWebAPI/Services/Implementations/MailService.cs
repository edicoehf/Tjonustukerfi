using System;
using System.Drawing;
using BarcodeLib;
using RestSharp;
using RestSharp.Authenticators;
using ThjonustukerfiWebAPI.EnvironmentVariables;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Setup;

namespace ThjonustukerfiWebAPI.Services.Implementations
{
    public static class MailService
    {
        public static void SendOrderNotification(OrderDTO order, CustomerDetailsDTO customer, double weeksSinceReady)
        {
            var emailAddress = customer.Email;
            var subject = "Reykofninn - Áminning fyrir tilbúna pöntun";
            var body = $"Góðan daginn {order.Customer}\n";
            body += $"Þetta er áminning um að þú eigir ósótta pöntun (nr. {order.Id}) sem kláraðist fyrir {weeksSinceReady} vikum.\nPöntun:\n";
            foreach (var item in order.Items)
            {
                body += $"\t\u2022 {item.Category} - {item.Service} - staða: {item.State}\n";
            }

            body += "\nKær kveðja Reykofninn";

            var bCode = new Barcode(); // get barcode lib class

            // encode to image
            bCode.Encode(BarcodeLib.TYPE.CODE128, order.Barcode, Color.Black, Color.White, BarcodeImageDimensions.Width, BarcodeImageDimensions.Height);
            var barcodeImageBase64 = Convert.ToBase64String(bCode.Encoded_Image_Bytes);

            MailService.Sendmail(emailAddress, subject, body, barcodeImageBase64);
        }

        public static void sendOrderComplete(OrderDTO order, CustomerDetailsDTO customer)
        {
            var emailAddress = customer.Email;
            var subject = "Reykofninn - pöntunin þín er klár.";
            var body = $"Góðan daginn {order.Customer}\n";
            // hérna kannski bæta við mynd af barkóða til að skanna, annars er order id númer pöntunarinnar sem viðskiptavinur gæti sagt við starfsmann
            body += $"Pöntunin þín (nr. {order.Id}) er tilbúin til afhendingar.\nPöntun:\n";
            foreach (var item in order.Items)
            {
                body += $"\t\u2022 {item.Category} - {item.Service} - staða: {item.State}\n";
            }

            body += "Kær kveðja Reykofninn";

            var bCode = new Barcode(); // get barcode lib class

            // encode to image
            bCode.Encode(BarcodeLib.TYPE.CODE128, order.Barcode, Color.Black, Color.White, BarcodeImageDimensions.Width, BarcodeImageDimensions.Height);
            var barcodeImageBase64 = Convert.ToBase64String(bCode.Encoded_Image_Bytes);

            MailService.Sendmail(emailAddress, subject, body, barcodeImageBase64);
        }
        private static IRestResponse Sendmail (string emailAddress, string subject, string body, string OrderBarcodeBase64Image = null)
        {
            var env = EnvironmentFileManager.LoadEvironmentFile();      // load env variables
            string apiKey;
            string apiUrl;
            env.TryGetValue("MAILGUN_AUTHENTICATION_KEY", out apiKey);  // get api key
            env.TryGetValue("MAILGUN_URL", out apiUrl);                 // get api urdl
            // if environmental variables are not set correctly throw exception
            if(apiKey == null || apiUrl == null) { throw new EmailException($"Could not send email. Could not find url or authentication key for the email request."); }
            
            RestClient client = new RestClient ();
            client.BaseUrl = new Uri (apiUrl);
            client.Authenticator = new HttpBasicAuthenticator ("api", apiKey);

            RestRequest request = new RestRequest ();
            request.AddParameter ("from", "Mailgun Sandbox <postmaster@sandboxfd3dcd967775490d82138a8f336fb6b2.mailgun.org>");
            request.AddParameter ("to", emailAddress);
            request.AddParameter ("subject", subject);
            request.AddParameter ("text", body);
            // add image if there is one.
            if(OrderBarcodeBase64Image != null)
            {
                request.AddParameter ("html", $"<html>Order: <img src=\"data:image/jpeg;base64, {OrderBarcodeBase64Image}\"></html>");
            }
            request.Method = Method.POST;

            return client.Execute (request);
        }
    }
}