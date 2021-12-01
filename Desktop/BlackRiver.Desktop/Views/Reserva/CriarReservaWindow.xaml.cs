using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for CriarReservaWindow.xaml
    /// </summary>
    public partial class CriarReservaWindow : Window
    {
        private List<Hospede> hospedes;
        private List<Quarto> quartos;
        private List<Reserva> reservas;

        public CriarReservaWindow()
        {
            InitializeComponent();
            MouseDown += delegate { this.SafeDragMove(); };
            GetData();
        }

        private async void GetData()
        {
            hospedes = await BlackRiverAPI.GetHospedes();
            quartos = await BlackRiverAPI.GetQuartos();
            reservas = await BlackRiverAPI.GetReservas();
            ChangeValor(false);
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

                var dataEntrada = data.AddHours(time.Hour).AddMinutes(time.Minute).ToUniversalTime();
                var dataSaida = data.AddDays(dias);

                var hospede = hospedes.FirstOrDefault(h => h.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (hospede == null)
                {
                    BlackRiverExtensions.ShowMessage("Não existe hóspede com esse email", "Erro");
                    return;
                }

                var reservasAtivas = reservas.Where(r =>
                r.Status.Equals(ReservaStatus.Aberto.ToString()) &&
                (
                    (dataSaida < r.DataSaida && dataSaida > r.DataEntrada) || (dataEntrada < r.DataSaida && dataEntrada > r.DataEntrada))
                );

                var reservasFilter = reservasAtivas.Where(r => r.HospedeId == hospede.Id);

                if (reservasFilter.Any())
                {
                    BlackRiverExtensions.ShowMessage("Já existe uma reserva ativa desse hóspede na data escolhida", "Erro");
                    return;
                }

                var quartosAtivos = quartos.Where(q => q.StatusQuarto == (int)QuartoStatus.Disponivel);

                var quarto = quartosAtivos.FirstOrDefault(q => !reservasAtivas.Any(r => r.QuartoId == q.Id));

                if (quarto == null)
                {
                    BlackRiverExtensions.ShowMessage("Não há quartos disponíveis", "Erro");
                    return;
                }

                var newReserva = new Reserva
                {
                    DataEntrada = dataEntrada,
                    DataSaida = dataSaida,
                    Status = ReservaStatus.Aberto.ToString(),
                    ValorDiaria = decimal.Parse(txtBoxNovaReservaValor.Text),
                    HospedeId = hospede.Id,
                    QuartoId = quarto.Id,
                    QuantidadePessoas = int.Parse(txtBoxNovaReservaQtdPessoas.Text),
                };

                var reservaResult = await BlackRiverAPI.CreateReserva(newReserva);

                if (reservaResult == null)
                    return;

                BlackRiverExtensions.ShowMessage("Reserva criada", "Sucesso");
                Close();
            }
            catch (Exception ex)
            {
                BlackRiverExtensions.ShowMessage(ex.Message, "Erro");
            }
        }

        public void ChangeValor(bool vip)
        {
            if (quartos == null || quartos.Count == 0)
            {
                txtBoxNovaReservaValor.IsEnabled = true;
                return;
            }

            var quartosDisponiveis = quartos
                    .Where(q => q.StatusQuarto == (int)QuartoStatus.Disponivel && q.TipoQuarto == (int)(vip ? QuartoTypes.Vip : QuartoTypes.Normal));

            var valor = quartosDisponiveis
                .Sum(q => q.ValorQuarto / quartosDisponiveis.Count());

            txtBoxNovaReservaValor.IsEnabled = false;
            txtBoxNovaReservaValor.Text = valor.ToString();
        }

        private void checkNovaReservaVIP_Checked(object sender, RoutedEventArgs e)
        {
            ChangeValor(true);
        }

        private void checkNovaReservaVIP_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeValor(false);
        }
    }
}
