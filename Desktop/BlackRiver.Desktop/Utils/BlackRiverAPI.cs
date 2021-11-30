using BlackRiver.Desktop.Extensions;
using BlackRiver.Desktop.Views;
using BlackRiver.EntityModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

        public static string MunicipiosUri => "Municipio/";
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

        #region ExternalAPIs

        public static async Task<List<Estado>> ExternalGetEstados()
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://servicodados.ibge.gov.br/api/v1/") })
            {
                var result = await client.GetAsync("localidades/estados/");
                var siglas = JsonConvert.DeserializeObject<List<Estado>>(await result.Content.ReadAsStringAsync());
                return siglas.OrderBy(e => e.Sigla).ToList();
            }
        }

        public static async Task<List<Cidade>> ExternalGetCidades(string uf)
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://brasilapi.com.br/api/ibge/municipios/v1/") })
            {
                var result = await client.GetAsync(uf);
                var cidades = JsonConvert.DeserializeObject<List<Cidade>>(await result.Content.ReadAsStringAsync());
                return cidades.OrderBy(c => c.Nome).ToList();
            }
        }

        #endregion ExternalAPIs

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
        public static async Task<Reserva> CreateHospede(Hospede hospede)
        {
            var reservaHospede = new
            {
            };

            var reservaAnon = new
            {
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

        //Update hospede

        #endregion Hospede

        #region Reserva

        public static async Task<List<Reserva>> GetReservas()
        {
            var response = await Client.GetAsync(ReservaUri);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao carregar Reservas", "Erro");
                return null;
            }

            return JsonConvert.DeserializeObject<List<Reserva>>(await response.Content.ReadAsStringAsync());
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

        //Get funcionario
        public static async Task<List<Funcionario>> GetFuncionarios()
        {
            var response = await Client.GetAsync(FuncionarioUri);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao carregar funcionários", "Error");
                return null;
            }

            return JsonConvert.DeserializeObject<List<Funcionario>>(await response.Content.ReadAsStringAsync());
        }

        //Create funcionario
        public static async Task<Funcionario> CreateFuncionario(Funcionario funcionario)
        {
            var content = JsonConvert.SerializeObject(funcionario);
            var response = await Client.PostAsync(FuncionarioUri, new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json));

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao criar funcionário", "Erro");
                return null;
            }

            return JsonConvert.DeserializeObject<Funcionario>(await response.Content.ReadAsStringAsync());
        }

        //Update funcionario
        public static async Task<Funcionario> UpdateFuncionario(Funcionario funcionario)
        {
            var json = JsonConvert.SerializeObject(funcionario);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PutAsync(QuartosUri + funcionario.Id, content);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao atualizar funcionário", "Erro");
                return null;
            }
            BlackRiverExtensions.ShowMessage("Funcionário foi atualizado", "Sucesso");
            return JsonConvert.DeserializeObject<Funcionario>(await response.Content.ReadAsStringAsync());
        }

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
                var response = await Client.PostAsync(QuartosUri, new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json));

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
            var response = await Client.PutAsync(QuartosUri + quarto.Id, content);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao atualizar quarto", "Erro");
                return null;
            }
            BlackRiverExtensions.ShowMessage("Quarto foi atualizado", "Sucesso");
            return JsonConvert.DeserializeObject<Hotel>(await response.Content.ReadAsStringAsync());
        }

        #endregion Quarto

        #region Municipio

        //Get municipio
        public static async Task<List<Municipio>> GetMunicipios()
        {
            var response = await Client.GetAsync(MunicipiosUri);

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage("Falha ao carregar municipios", "Erro");
                return null;
            }

            return JsonConvert.DeserializeObject<List<Municipio>>(await response.Content.ReadAsStringAsync());
        }

        //Create Municipio
        public static async Task<Municipio> CreateMunicipio(Municipio municipio)
        {
            var content = JsonConvert.SerializeObject(municipio);
            var response = await Client.PostAsync(MunicipiosUri, new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json));

            if (!response.IsSuccessStatusCode)
            {
                BlackRiverExtensions.ShowMessage($"Falha ao criar Municipio", "Erro");
                return null;
            }

            return JsonConvert.DeserializeObject<Municipio>(await response.Content.ReadAsStringAsync());
        }

        #endregion Municipio
    }
}
