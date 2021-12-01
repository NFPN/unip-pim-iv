using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System;
using System.Linq;
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
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void btnNovaReservaCriar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = dateNovaReserva.SelectedDate.GetValueOrDefault();
                var time = timeNovaReserva.SelectedTime.GetValueOrDefault();
                var dias = int.Parse(txtBoxNovaReservaQtdDias.Text);
                var email = txtBoxNovaReservaEmail.Text;

                var hospedes = await BlackRiverAPI.GetHospedes();
                var hospede = hospedes.FirstOrDefault(h => h.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                var reserva = new Reserva
                {
                    DataEntrada = data.AddHours(time.Hour).AddMinutes(time.Minute).ToUniversalTime(),
                    DataSaida = data.AddDays(dias),
                    Status = ReservaStatus.Aberto.ToString(),
                    ValorDiaria = decimal.Parse(txtBoxNovaReservaValor.Text),
                    HospedeId = hospede.Id,
                };

                var result = await BlackRiverAPI.CreateReserva(reserva);

                if (result == null)
                    return;

                BlackRiverExtensions.ShowMessage("Reserva criada", "Sucesso");
                Close();
            }
            catch (Exception ex)
            {
                BlackRiverExtensions.ShowMessage(ex.Message, "Error");
            }
        }
    }
}
