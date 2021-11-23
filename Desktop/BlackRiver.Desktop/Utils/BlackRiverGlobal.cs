using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BlackRiver.Desktop
{
    public static class BlackRiverGlobal
    {
        public static bool IsAdminLogin { get; set; }
        public static string LoginToken { get; set; } = string.Empty;

        public static HttpClient APIClient
        {
            get
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(ConfigurationManager.AppSettings["api"]),
                };

                if (!string.IsNullOrWhiteSpace(LoginToken))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginToken);

                return client;
            }
        }

        public static string AuthUserUri => "Account/authuser/";
        public static string LoginUri => "Account/login/";
        public static string UpdateLoginUri => "Account/update/";
        public static string RegisterUri => "Account/register/";

        public static string FirstUseFolder = @$"{ Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) }\BlackRiver";
        public static string FirstUseFile = @$"{FirstUseFolder}\first.bin";
    }
}
