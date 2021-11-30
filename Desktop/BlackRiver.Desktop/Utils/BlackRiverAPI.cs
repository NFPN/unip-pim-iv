using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
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
        public static string QuartosUri => "Quarto/";

        public static string HospedeUri => "Hospede/";
        public static string ReservaUri => "Reserva/";
        public static string OcorrenciaUri => "Ocorrencia/";

        public static string ProdutoUri => "Produto/";
        public static string ProdutoCategoriaUri => "ProdutoCategoria/";
        public static string VagasUri => "VagasEstacionamento/";
        public static string VendasUri => "Venda/";

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

            BlackRiverExtensions.ShowMessage("Falha ao carregar usuário", "Erro");
            return null;
        }

        #endregion Account

        #region Hospede

        public static async Task<List<Hospede>> GetHospedes()
        {
            var response = await Client.GetAsync(HospedeUri);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao carregar hóspede", "Erro");
                return null;
            }

            return JsonConvert.DeserializeObject<List<Hospede>>(await response.Content.ReadAsStringAsync());
        }

        //Create hospede
        //Update hospede

        #endregion Hospede

        #region Reserva

        public static async Task<Reserva> GetReservas()
        {
            var response = await Client.GetAsync(ReservaUri);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao carregar Reservas", "Erro");
                return null;
            }

            return JsonConvert.DeserializeObject<Reserva>(await response.Content.ReadAsStringAsync());
        }

        //Create reserva

        public static async Task<Reserva> CreateReserva(Reserva reserva)
        {
            var reservaHospede = new
            {
            };

            var reservaAnon = new
            {
                reserva.DataEntrada,
            };

            var content = JsonConvert.SerializeObject(reservaAnon);
            var response = await Client.PostAsJsonAsync(ReservaUri, content);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao criar reserva", "Erro");
                return null;
            }

            return JsonConvert.DeserializeObject<Reserva>(await response.Content.ReadAsStringAsync());
        }

        //Update reserva

        #endregion Reserva

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

        //Create funcionario
        public static async Task<Hotel> CreateFuncionario(Funcionario funcionario)
        {
            var funcionarioLogin = new
            {
                Username = funcionario.Email,
                Password = funcionario.RG,
                Type = funcionario.Login.Type,
            };

            var funcionarioMunicipio = new
            { 

            };

            var funcionarioHotel = new
            {

            };

            var funcionarioAnon = new
            {
                funcionario.Nome,
                funcionario.CPF,
                funcionario.RG,
                funcionario.Email,
                funcionario.CTPS,
                funcionario.DataNascimento,
                funcionario.Telefone,
                funcionario.Endereco,
                funcionario.Cargo,
                funcionario.Departamento,
                funcionario.HotelAtual,
                funcionarioLogin,
                funcionario.MunicipioAtual,
            };

            var content = JsonConvert.SerializeObject(funcionarioAnon);

            var response = await Client.PostAsJsonAsync(FuncionarioUri, content);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao criar funcionário", "Erro");
                return null;
            }

            return JsonConvert.DeserializeObject<Hotel>(await response.Content.ReadAsStringAsync());
        }

        //Update funcionario

        #endregion Funcionario

        #region Hotel

        //Get hotel
        public static async Task<Hotel> GetHotel()
        {
            var response = await Client.GetAsync(HotelUri + 1);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao carregar dados do hotel", "Erro");
                return null;
            }

            return JsonConvert.DeserializeObject<Hotel>(await response.Content.ReadAsStringAsync());
        }

        //Create hotel
        public static async Task<Hotel> CreateHotel(string nome, string endereco, Municipio municipio)
        {
            var municipioAnon = new
            {
                municipio.Nome,
                municipio.UF
            };

            var content = JsonConvert.SerializeObject(new
            {
                nome,
                endereco,
                municipioAnon
            });

            var response = await Client.PostAsJsonAsync(HotelUri + 1, content);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao criar hotel", "Erro");
                return null;
            }

            return JsonConvert.DeserializeObject<Hotel>(await response.Content.ReadAsStringAsync());
        }

        //Update hotel
        public static async Task<Hotel> UpdateHotel(Hotel hotel)
        {
            var json = JsonConvert.SerializeObject(hotel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(HotelUri + 1, content);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao atualizar hotel", "Erro");
                return null;
            }
            BlackRiverExtensions.ShowMessage("Hotel foi atualizado", "Sucesso");
            return JsonConvert.DeserializeObject<Hotel>(await response.Content.ReadAsStringAsync());
        }

        #endregion Hotel

        #region Quarto

        //Get quartos
        public static async Task<List<Quarto>> GetQuartos()
        {
            var response = await Client.GetAsync(QuartosUri);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao carregar quartos do hotel", "Erro");
                return null;
            }

            return JsonConvert.DeserializeObject<List<Quarto>>(await response.Content.ReadAsStringAsync());
        }

        //Create quarto
        public static async Task<List<Quarto>> CreateQuarto(Quarto quarto, int quantity)
        {
            var responses = new List<Quarto>();
            var count = 0;
            for (int i = 0; i < quantity; i++)
            {
                var quartoAnon = new
                {
                    quarto.TipoQuarto,
                    quarto.NumeroAndar,
                    quarto.ValorQuarto,
                    quarto.StatusQuarto,
                    quarto.Vip,
                };

                var content = JsonConvert.SerializeObject(quartoAnon);
                var response = await Client.PostAsync(QuartosUri, new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json)) ;

                if (!response.IsSuccessStatusCode)
                {
                    BlackRiverExtensions.ShowMessage($"Falha ao criar quarto(s)\n Quartos criados - {count}", "Erro");
                    return null;
                }

                responses.Add(JsonConvert.DeserializeObject<Quarto>(await response.Content.ReadAsStringAsync()));
                count++;
            }

            return responses;
        }

        //Update quarto
        public static async Task<Hotel> UpdateQuarto(Quarto quarto)
        {
            var json = JsonConvert.SerializeObject(quarto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(QuartosUri, content);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao atualizar quarto", "Erro");
                return null;
            }
            BlackRiverExtensions.ShowMessage("Quarto foi atualizado", "Sucesso");
            return JsonConvert.DeserializeObject<Hotel>(await response.Content.ReadAsStringAsync());
        }

        #endregion Quarto
    }
}
