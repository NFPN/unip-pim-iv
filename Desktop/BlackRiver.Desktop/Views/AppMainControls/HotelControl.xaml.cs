using BlackRiver.EntityModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for HotelControl.xaml
    /// </summary>
    public partial class HotelControl : UserControl, IControlUpdate
    {
        private Hotel hotelAtual = new();
        private List<string> cidades = new();

        public HotelControl()
        {
            InitializeComponent();
        }

        public async void UpdateControlData()
        {
            if (comboHotelEstado.Items.Count == 0)
            {
                foreach (var estado in await BlackRiverAPI.ExternalGetEstados())
                    comboHotelEstado.Items.Add(estado.Sigla);

                comboHotelEstado.SelectedValue = comboHotelEstado.Items[0];

                foreach (var cidade in await BlackRiverAPI.ExternalGetCidades(comboHotelEstado.SelectedValue.ToString()))
                    cidades.Add(cidade.Nome);

                cidades.OrderBy(s => s).ToList().ForEach(s => comboHotelCidade.Items.Add(s));
            }

            comboHotelEstado.UpdateLayout();
            comboHotelCidade.UpdateLayout();

            CancellationTokenSource tokenSource = new CancellationTokenSource();

            UpdateHotelAtualLayout();
            await Application.Current.Dispatcher.Invoke(async delegate
            {
                await Task.Delay(1000);
                UpdateHotelAtualLayout();
            });
        }

        private async void UpdateHotelAtualLayout()
        {
            hotelAtual = await BlackRiverAPI.GetHotel();
            var municipios = await BlackRiverAPI.GetMunicipios();
            var municipio = municipios.FirstOrDefault(m => m.Id == hotelAtual.MunicipioId);

            txtBoxHotelNome.Text = hotelAtual.Nome;
            txtBoxHotelEndereco.Text = hotelAtual.Endereco;

            comboHotelEstado.SelectedValue = municipio.UF;
            comboHotelCidade.SelectedValue = municipio.Nome;
        }

        private async void comboHotelEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboHotelCidade.Items.Clear();
            foreach (var cidade in await BlackRiverAPI.ExternalGetCidades(comboHotelEstado.SelectedValue.ToString()))
            {
                if (comboHotelCidade.Items.Count == 1)
                    comboHotelCidade.SelectedValue = comboHotelCidade.Items[0];

                comboHotelCidade.Items.Add(cidade.Nome);
            }
            comboHotelCidade.UpdateLayout();
            comboHotelCidade.Items.Refresh();
        }

        private async void btnHotelUpdate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            hotelAtual.Id = 1;
            hotelAtual.Nome = txtBoxHotelNome.Text;
            hotelAtual.Endereco = txtBoxHotelEndereco.Text;

            var municipios = await BlackRiverAPI.GetMunicipios();

            if (municipios.Any(m => m.Nome.Equals(comboHotelCidade.SelectedItem.ToString())))
                hotelAtual.MunicipioId = municipios.FirstOrDefault(m => m.Nome.Equals(comboHotelCidade.SelectedItem.ToString())).Id;
            else
            {
                var mun = await BlackRiverAPI.CreateMunicipio(new Municipio
                {
                    Nome = comboHotelCidade.SelectedItem.ToString(),
                    UF = comboHotelEstado.SelectedItem.ToString(),
                });

                hotelAtual.MunicipioId = mun.Id;
            }

            hotelAtual = await BlackRiverAPI.UpdateHotel(hotelAtual);
            UpdateControlData();
            UpdateLayout();
        }
    }
}
