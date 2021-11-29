using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CriarQuartoWindow.xaml
    /// </summary>
    public partial class CriarQuartoWindow : Window, IControlUpdate
    {
        public CriarQuartoWindow()
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
            UpdateControlData();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnAddQuartoAdicionar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var quarto = new Quarto
                {
                    NumeroAndar = int.Parse(txtBoxAddQuartoAndar.Text),
                    StatusQuarto = (int)(QuartoStatus)comboAddQuartoStatus.SelectedItem,
                    TipoQuarto = (int)(QuartoTypes)comboAddQuartoTipo.SelectedItem,
                    ValorQuarto = decimal.Parse(txtBoxAddQuartoValorDiaria.Text),
                    Vip = (QuartoTypes)comboAddQuartoTipo.SelectedItem == QuartoTypes.Vip,
                };


                await BlackRiverAPI.CreateQuarto(quarto, int.Parse(txtBoxAddQuartoQtd.Text));
            }
            catch (Exception ex)
            {
                BlackRiverExtensions.ShowMessage(ex.Message, "Erro");
            }
        }

        public void UpdateControlData()
        {
            foreach (var item in Enum.GetValues(typeof(QuartoStatus)))
                comboAddQuartoStatus.Items.Add(item);

            foreach (var item in Enum.GetValues(typeof(QuartoTypes)))
                comboAddQuartoTipo.Items.Add(item);

            comboAddQuartoStatus.UpdateLayout();
        }
    }
}
