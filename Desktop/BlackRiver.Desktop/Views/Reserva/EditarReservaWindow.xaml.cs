using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditarReservaWindow.xaml
    /// </summary>
    public partial class EditarReservaWindow : Window
    {
        public Reserva Reserva { get; }

        public EditarReservaWindow(Reserva reserva = null)
        {
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
            Reserva = reserva;
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnEditReservaAlterar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dataIn = dateEditReservaCheckIn.SelectedDate.GetValueOrDefault();
                var dataOut = dateEditReservaCheckOut.SelectedDate.GetValueOrDefault();

                int dias = int.Parse(txtBoxEditReservaQtdDias.Text);
                var reserva = new Reserva
                {
                    DataEntrada = dataIn.AddHours(dataOut.Hour).AddMinutes(dataOut.Minute).ToUniversalTime(),
                    DataSaida = dataIn.AddDays(dias),
                    Status = ReservaStatus.Aberto.ToString(),
                    ValorDiaria = Reserva.ValorDiaria,
                };

                var result = await BlackRiverAPI.CreateReserva(reserva);

                if (result == null)
                    return;

                BlackRiverExtensions.ShowMessage("Reserva atualizada", "Sucesso");
                Close();
            }
            catch (Exception) { }
        }
    }
}
