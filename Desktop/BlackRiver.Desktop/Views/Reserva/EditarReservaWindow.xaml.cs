using BlackRiver.EntityModels;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditarReservaWindow.xaml
    /// </summary>
    public partial class EditarReservaWindow : Window
    {
        public EditarReservaWindow(Reserva reserva = null)
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnEditReservaAlterar_Click(object sender, RoutedEventArgs e)
        {
            var dataIn = dateEditReservaCheckIn.SelectedDate.GetValueOrDefault();
            var dataOut = dateEditReservaCheckOut.SelectedDate.GetValueOrDefault();

            int dias = int.Parse(txtBoxNovaReservaQtdDias.Text);
            var reserva = new Reserva
            {
                DataEntrada = dataIn.AddHours(dataOut.Hour).AddMinutes(dataOut.Minute).ToUniversalTime(),
                DataSaida = dataIn.AddDays(dias),
                Status = ReservaStatus.Aberto.ToString(),
                ValorDiaria = decimal.Parse(txtBoxNovaReservaValor.Text),
                
            };

            await BlackRiverAPI.CreateReserva(reserva);
        }
    }
}
