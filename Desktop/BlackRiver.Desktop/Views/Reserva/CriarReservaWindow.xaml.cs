using BlackRiver.EntityModels;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CriarReservaWindow.xaml
    /// </summary>
    public partial class CriarReservaWindow : Window
    {
        public CriarReservaWindow()
        {
            MouseDown += delegate { DragMove(); };
            InitializeComponent();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnNovaReservaCriar_Click(object sender, RoutedEventArgs e)
        {
            var data = dateNovaReserva.SelectedDate.GetValueOrDefault();
            var time = timeNovaReserva.SelectedTime.GetValueOrDefault();

            int dias = int.Parse(txtBoxNovaReservaQtdDias.Text);
            var reserva = new Reserva
            {
                DataEntrada = data.AddHours(time.Hour).AddMinutes(time.Minute).ToUniversalTime(),
                DataSaida = data.AddDays(dias),
                Status = ReservaStatus.Aberto.ToString(),
                ValorDiaria = decimal.Parse(txtBoxNovaReservaValor.Text)
            };

            await BlackRiverAPI.CreateReserva(reserva);
        }
    }
}
