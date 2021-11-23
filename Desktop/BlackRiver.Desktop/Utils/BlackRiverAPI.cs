using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BlackRiver.Desktop
{
    public static class BlackRiverAPI
    {
        public static string Token { get; set; } = string.Empty;

        public static HttpClient Client
        {
            get
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ConfigurationManager.AppSettings["api"]),
                };

                if (!string.IsNullOrWhiteSpace(Token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                return client;
            }
        }

        public static string AuthUserUri => "Account/authuser/";
        public static string LoginUri => "Account/login/";
        public static string UpdateLoginUri => "Account/update/";
        public static string RegisterUri => "Account/register/";
    }
}
