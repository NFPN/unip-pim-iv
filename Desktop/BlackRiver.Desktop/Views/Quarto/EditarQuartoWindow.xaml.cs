using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditarQuartoWindow.xaml
    /// </summary>
    public partial class EditarQuartoWindow : Window
    {
        private Quarto EditQuarto { get; set; }

        public EditarQuartoWindow(Quarto quarto = null)
        {
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
            EditQuarto = quarto;
            UpdateControlData();

            txtBoxEditQuartoAndar.Text = EditQuarto.NumeroAndar.ToString();
            comboEditQuartoStatus.SelectedItem = (QuartoStatus)EditQuarto.StatusQuarto;
            comboEditQuartoTipo.SelectedItem = (QuartoTypes)EditQuarto.TipoQuarto;
            txtBoxEditQuartoValorDiaria.Text = EditQuarto.ValorQuarto.ToString();
        }

        private async void btnEditQuartoAdicionar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditQuarto.NumeroAndar = int.Parse(txtBoxEditQuartoAndar.Text);
                EditQuarto.StatusQuarto = (int)(QuartoStatus)comboEditQuartoStatus.SelectedItem;
                EditQuarto.TipoQuarto = (int)(QuartoTypes)comboEditQuartoTipo.SelectedItem;
                EditQuarto.ValorQuarto = decimal.Parse(txtBoxEditQuartoValorDiaria.Text);
                EditQuarto.Vip = (QuartoTypes)EditQuarto.TipoQuarto == QuartoTypes.Vip;

                var result = await BlackRiverAPI.UpdateQuarto(EditQuarto);

                if (result != null)
                    return;

                Close();
            }
            catch (Exception ex)
            {
                BlackRiverExtensions.ShowMessage(ex.Message, "Erro");
            }
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void UpdateControlData()
        {
            foreach (var item in Enum.GetValues(typeof(QuartoStatus)))
                comboEditQuartoStatus.Items.Add(item);

            foreach (var item in Enum.GetValues(typeof(QuartoTypes)))
                comboEditQuartoTipo.Items.Add(item);

            comboEditQuartoStatus.UpdateLayout();
            comboEditQuartoTipo.UpdateLayout();
        }
    }
}
