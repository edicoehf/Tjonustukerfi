using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using RestSharp.Authenticators;
using ThjonustukerfiWebAPI.Models.DTOs;

namespace ThjonustukerfiWebAPI.Services.Implementations
{
    public static class MailService
    {
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

            body += "Kær kveðja reykofninn";
            
            MailService.Sendmail(emailAddress, subject, body);
        }
        private static IRestResponse Sendmail (string emailAddress, string subject, string body)
        {
            RestClient client = new RestClient ();
            client.BaseUrl = new Uri ("https://api.mailgun.net/v3/sandboxfd3dcd967775490d82138a8f336fb6b2.mailgun.org/messages");
            client.Authenticator = new HttpBasicAuthenticator ("api", "97ee06aae75d080a18d280122fcadc89-816b23ef-ebe3f667");

            RestRequest request = new RestRequest ();
            request.AddParameter ("from", "Mailgun Sandbox <postmaster@sandboxfd3dcd967775490d82138a8f336fb6b2.mailgun.org>");
            request.AddParameter ("to", emailAddress);
            request.AddParameter ("subject", subject);
            request.AddParameter ("text", body);
            request.Method = Method.POST;

            return client.Execute (request);
        }
    }
}