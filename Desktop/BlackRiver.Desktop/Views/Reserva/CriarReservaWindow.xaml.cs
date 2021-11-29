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

        private void btnNovaReservaCriar_Click(object sender, RoutedEventArgs e)
        {
            //get quartos
            //get hospedes
            //compare & vip

            var data = dateNovaReserva.SelectedDate.GetValueOrDefault();
            var reserva = new Reserva
            {
                DataEntrada = data.ToUniversalTime(),
                DataSaida = data.AddDays(int.Parse(txtBoxNovaReservaQtdDias.Text)),
                Status = ReservaStatus.Aberto.ToString(),
            };

            BlackRiverAPI.CreateReserva(reserva);

            //TODO: call API 
        }
    }
}
