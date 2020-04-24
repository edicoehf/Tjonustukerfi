using System;
using RestSharp;
using RestSharp.Authenticators;

namespace ThjonustukerfiWebAPI.Services.Implementations
{
    public static class MailService
    {
        public static IRestResponse Sendmail (string emailTo, string subject, string body)
        {
            RestClient client = new RestClient ();
            client.BaseUrl = new Uri ("https://api.mailgun.net/v3/sandboxfd3dcd967775490d82138a8f336fb6b2.mailgun.org/messages");
            client.Authenticator =
                new HttpBasicAuthenticator ("api",
                    "97ee06aae75d080a18d280122fcadc89-816b23ef-ebe3f667");
            RestRequest request = new RestRequest ();
            request.AddParameter ("from", "Mailgun Sandbox <postmaster@sandboxfd3dcd967775490d82138a8f336fb6b2.mailgun.org>");
            request.AddParameter ("to", emailTo);
            request.AddParameter ("subject", subject);
            request.AddParameter ("text", body);
            request.Method = Method.POST;
            return client.Execute (request);
        }
    }
}