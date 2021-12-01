using BlackRiver.EntityModels;
using System.Linq;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for HotelControl.xaml
    /// </summary>
    public partial class HotelControl : UserControl, IControlUpdate
    {
        private Hotel hotelAtual = new();

        public HotelControl()
        {
            InitializeComponent();
            UpdateControlData();
        }

        public async void UpdateControlData()
        {
            hotelAtual = await BlackRiverAPI.GetHotel();
            UpdateHotelAtualLayout();

            foreach (var estado in await BlackRiverAPI.ExternalGetEstados())
                comboHotelEstado.Items.Add(estado.Sigla);

            foreach (var cidade in await BlackRiverAPI.ExternalGetCidades(comboHotelEstado.SelectedValue.ToString()))
                comboHotelCidade.Items.Add(cidade.Nome);

            comboHotelEstado.UpdateLayout();
            comboHotelCidade.UpdateLayout();
        }

        private async void UpdateHotelAtualLayout()
        {
            txtBoxHotelNome.Text = hotelAtual.Nome;
            txtBoxHotelEndereco.Text = hotelAtual.Endereco;

            var municipios = await BlackRiverAPI.GetMunicipios();
            var municipio = municipios.FirstOrDefault(m => m.Id == hotelAtual.MunicipioId);

            comboHotelEstado.SelectedValue = municipio.UF;
            comboHotelCidade.SelectedValue = municipio.Nome;

            txtBoxHotelEndereco.UpdateLayout();
            comboHotelEstado.UpdateLayout();
            comboHotelCidade.UpdateLayout();
            txtBoxHotelNome.UpdateLayout();
        }

        private async void comboHotelEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboHotelCidade.Items.Clear();
            foreach (var cidade in await BlackRiverAPI.ExternalGetCidades(comboHotelEstado.SelectedValue.ToString()))
            {
                if (comboHotelCidade.Items.Count == 1)
                    comboHotelCidade.SelectedValue = comboHotelCidade.Items[0];

                comboHotelCidade.Items.Add(cidade.Nome);
                comboHotelCidade.UpdateLayout();
            }
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
            UpdateHotelAtualLayout();
        }
    }
}
