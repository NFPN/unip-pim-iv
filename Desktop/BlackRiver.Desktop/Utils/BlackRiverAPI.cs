using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlackRiver.Desktop
{
    public static class BlackRiverAPI
    {
        public static string Token { get; set; } = string.Empty;

        public static HttpClient Client
        {
            get
            {
                var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["api"]), };

                if (!string.IsNullOrWhiteSpace(Token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                return client;
            }
        }

        public static string AuthUserUri => "Account/authuser/";
        public static string LoginUri => "Account/login/";
        public static string UpdateLoginUri => "Account/update/";
        public static string RegisterUri => "Account/register/";

        public static string HotelUri => "Hotel/";
        public static string FuncionarioUri => "Funcionario/";

        public static string HospedeUri => "Hospede/";
        public static string ReservaUri => "Reserva/";

        public static string ProdutoUri => "Produto/";
        public static string VagasUri => "VagasEstacionamento/";
        public static string VendasUri => "Vendas/";

        #region Account

        public static async Task<bool> FetchToken(string username, string password)
        {
            var tokenResponse = await Client.GetAsync($"{LoginUri}?username={username}&password={password}");

            if (!tokenResponse.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Login Incorreto", "Error");
                return false;
            }

            var token = JsonConvert.DeserializeObject<APIToken>(await tokenResponse.Content.ReadAsStringAsync());
            Token = token.Token;
            return true;
        }

        public static async Task<UserLogin> GetLoggedUser()
        {
            var response = await Client.GetAsync(AuthUserUri);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<UserLogin>(await response.Content.ReadAsStringAsync());

            BlackRiverExtensions.ShowMessage("Falha ao carregar usuário", "Error");
            return null;
        }

        #endregion Account

        #region Hospede

        public static async Task<List<Hospede>> GetHospedes()
        {
            var response = await Client.GetAsync(HospedeUri);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao carregar hóspede", "Error");
                return null;
            }

            return JsonConvert.DeserializeObject<List<Hospede>>(await response.Content.ReadAsStringAsync());
        }

        public static async Task<Reserva> GetReservas()
        {
            var response = await Client.GetAsync(ReservaUri);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao carregar Reservas", "Error");
                return null;
            }

            return JsonConvert.DeserializeObject<Reserva>(await response.Content.ReadAsStringAsync());
        }

        #endregion Hospede



        #region Funcionario

        public static async Task<UserLogin> GetFuncionarios()
        {
            var response = await Client.GetAsync(AuthUserUri);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao carregar funcionários", "Error");
                return null;
            }

            return JsonConvert.DeserializeObject<UserLogin>(await response.Content.ReadAsStringAsync());
        }

        #endregion Funcionario
    }
}
