using BlackRiver.EntityModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for HotelControl.xaml
    /// </summary>
    public partial class HotelControl : UserControl, IControlUpdate
    {
        Hotel hotelAtual;
        private class Estado
        {
            public string Sigla { get; set; }
        }

        private class Cidade
        {
            public string Nome { get; set; }
        }

        public HotelControl()
        {
            InitializeComponent();
            UpdateControlData();
        }

        public async void UpdateControlData()
        {
            hotelAtual = await BlackRiverAPI.GetHotel();
            UpdateHotelAtualLayout();

            foreach (var estado in await GetEstados())
                comboHotelEstado.Items.Add(estado.Sigla);

            foreach (var cidade in await GetCidades(comboHotelEstado.SelectedValue.ToString()))
                comboHotelCidade.Items.Add(cidade.Nome);

            comboHotelEstado.UpdateLayout();
            comboHotelCidade.UpdateLayout();
        }

        private void UpdateHotelAtualLayout()
        {
            txtBoxHotelNome.Text = hotelAtual.Nome;
            txtBoxHotelEndereco.Text = hotelAtual.Endereco;
            comboHotelEstado.SelectedValue = hotelAtual.MunicipioAtual.UF;
            comboHotelCidade.SelectedValue = hotelAtual.MunicipioAtual.Nome;

            txtBoxHotelEndereco.UpdateLayout();
            comboHotelEstado.UpdateLayout();
            comboHotelCidade.UpdateLayout();
            txtBoxHotelNome.UpdateLayout();
        }

        private async Task<List<Estado>> GetEstados()
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://servicodados.ibge.gov.br/api/v1/") })
            {
                var result = await client.GetAsync("localidades/estados/");
                var siglas = JsonConvert.DeserializeObject<List<Estado>>(await result.Content.ReadAsStringAsync());
                return siglas.OrderBy(e => e.Sigla).ToList();
            }
        }

        private async Task<List<Cidade>> GetCidades(string uf)
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://brasilapi.com.br/api/ibge/municipios/v1/") })
            {
                var result = await client.GetAsync(uf);
                var cidades = JsonConvert.DeserializeObject<List<Cidade>>(await result.Content.ReadAsStringAsync());
                return cidades.OrderBy(c => c.Nome).ToList();
            }
        }

        private async void comboHotelEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboHotelCidade.Items.Clear();
            foreach (var cidade in await GetCidades(comboHotelEstado.SelectedValue.ToString()))
            {
                if (comboHotelCidade.Items.Count == 1)
                    comboHotelCidade.SelectedValue = comboHotelCidade.Items[0];

                comboHotelCidade.Items.Add(cidade.Nome);
                comboHotelCidade.UpdateLayout();
            }
        }

        private async void btnHotelUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (hotelAtual == null)
                return;

            hotelAtual.Nome = txtBoxHotelNome.Text;
            hotelAtual.Endereco = txtBoxHotelEndereco.Text;
            hotelAtual.MunicipioAtual = new Municipio
            {
                Id = hotelAtual.MunicipioAtual.Id,
                Nome = comboHotelCidade.SelectedValue.ToString(),
                UF = comboHotelEstado.SelectedValue.ToString(),
            };

            hotelAtual = await BlackRiverAPI.UpdateHotel(hotelAtual);
            UpdateHotelAtualLayout();
        }
    }
}
