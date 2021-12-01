using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System;
using System.Linq;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EditarReservaWindow.xaml
    /// </summary>
    public partial class EditarReservaWindow : Window
    {
        public Reserva ReservaData { get; }

        public EditarReservaWindow(Reserva reserva = null)
        {
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
            ReservaData = reserva;
            UpdateControlData();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void UpdateControlData()
        {
            if (ReservaData == null)
                return;

            var quartos = await BlackRiverAPI.GetQuartos();
            var hospedes = await BlackRiverAPI.GetHospedes();
            var hospede = hospedes.FirstOrDefault(h => h.Id == ReservaData.HospedeId);

            txtBoxEditReservaEmail.Text = hospede.Email;
            txtBoxEditReservaQtdDias.Text = ReservaData.DataSaida.Subtract(ReservaData.DataEntrada).Days.ToString();
            txtBoxEditReservaQtdPessoas.Text = ReservaData?.QuantidadePessoas.ToString() ?? string.Empty;

            dateEditReservaCheckIn.Text = ReservaData.DataEntrada.ToString();
            timeEditReservaCheckIn.Text = ReservaData.DataEntrada.TimeOfDay.ToString();

            dateEditReservaCheckOut.Text = ReservaData.DataSaida.ToString();
            timeEditReservaCheckOut.Text = ReservaData.DataEntrada.TimeOfDay.ToString();

            var status = Enum.Parse<ReservaStatus>(ReservaData.Status);

            checkEditReservaActive.IsChecked = status == ReservaStatus.Aberto ? true : false;
            checkEditReservaVIP.IsChecked = quartos.FirstOrDefault(q => q.Id == ReservaData.QuartoId).Vip;
        }

        private async void btnEditReservaAlterar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var timeIn = timeEditReservaCheckIn.SelectedTime.GetValueOrDefault();
                var timeOut = timeEditReservaCheckOut.SelectedTime.GetValueOrDefault();

                var dataIn = dateEditReservaCheckIn.SelectedDate.GetValueOrDefault().AddHours(timeIn.Hour).AddMinutes(timeIn.Minute);
                var dataOut = dateEditReservaCheckOut.SelectedDate.GetValueOrDefault().AddHours(timeOut.Hour).AddMinutes(timeOut.Minute);

                var dias = int.Parse(txtBoxEditReservaQtdDias.Text);
                var pessoas = int.Parse(txtBoxEditReservaQtdPessoas.Text);

                var status = checkEditReservaActive.IsChecked.GetValueOrDefault() ? ReservaStatus.Aberto : ReservaStatus.Cancelado;

                var reserva = new Reserva
                {
                    Id = ReservaData.Id,
                    DataEntrada = dataIn.ToUniversalTime(),
                    DataSaida = dataOut.ToUniversalTime(),
                    Status = status.ToString(),
                    ValorDiaria = ReservaData.ValorDiaria,
                    DataCancelamento = status == ReservaStatus.Cancelado ? DateTime.UtcNow : null,
                    QuantidadePessoas = pessoas,
                    HospedeId = ReservaData.HospedeId,
                    QuartoId = ReservaData.QuartoId,
                };

                var result = await BlackRiverAPI.UpdateReserva(reserva);

                if (result == null)
                    return;

                BlackRiverExtensions.ShowMessage("Reserva atualizada", "Sucesso");
                Close();
            }
            catch (Exception) { }
        }
    }
}
