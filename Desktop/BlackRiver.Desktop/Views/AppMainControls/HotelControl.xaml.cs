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

        private void UpdateHotelAtualLayout()
        {
            if (hotelAtual == null)
                return;

            txtBoxHotelNome.Text = hotelAtual.Nome;
            txtBoxHotelEndereco.Text = hotelAtual.Endereco;

            if (hotelAtual.MunicipioAtual != null)
            {
                comboHotelEstado.SelectedValue = hotelAtual.MunicipioAtual.UF;
                comboHotelCidade.SelectedValue = hotelAtual.MunicipioAtual.Nome;
            }

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

            var muns = await BlackRiverAPI.GetMunicipios();

            if (muns.Any(m => m.Nome.Equals(comboHotelCidade.SelectedItem.ToString())))
                hotelAtual.MunicipioAtual = muns.FirstOrDefault(m => m.Nome.Equals(comboHotelCidade.SelectedItem.ToString()));
            else
                hotelAtual.MunicipioAtual = await BlackRiverAPI.CreateMunicipio(new Municipio
                {
                    Nome = comboHotelCidade.SelectedItem.ToString(),
                    UF = comboHotelEstado.SelectedItem.ToString(),
                });

            hotelAtual = await BlackRiverAPI.UpdateHotel(hotelAtual);
            UpdateHotelAtualLayout();
        }
    }
}
