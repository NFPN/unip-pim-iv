using BlackRiver.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for DashboardControl.xaml
    /// </summary>
    public partial class DashboardControl : UserControl, IControlUpdate
    {
        private List<Reserva> currentReservas = new();
        private List<Hospede> currentHospedes = new();
        private List<Quarto> currentQuartos = new();
        private List<DashboardReservaDataRow> currentDatalist = new();

        public DashboardControl()
        {
            InitializeComponent();
        }

        public async void UpdateControlData()
        {
            currentReservas = await BlackRiverAPI.GetReservas();
            currentHospedes = await BlackRiverAPI.GetHospedes();
            currentQuartos = await BlackRiverAPI.GetQuartos();

            currentDatalist.Clear();

            foreach (var item in currentReservas)
            {
                currentDatalist.Add(new DashboardReservaDataRow
                {
                    Nome = currentHospedes.FirstOrDefault(h => h.Id == item.HospedeId).Nome,
                    Horário = item.DataEntrada,
                    NumeroQuarto = item.QuartoId,
                    Status = item.Status.Equals("Reservado", StringComparison.InvariantCultureIgnoreCase),
                });

                datagridDashboard.UpdateLayout();
            }

            var quartosOcupados = currentQuartos.Where(q => q.StatusQuarto is ((int)QuartoStatus.Ocupado) or ((int)QuartoStatus.Indisponível)).Count();
            var ocupacao = quartosOcupados == 0 ? 0 : (double)quartosOcupados / currentQuartos.Count;

            lblTotalQuartos.Content = currentQuartos.Count;
            lblDashboardQuartosDisp.Content = currentQuartos.Count - quartosOcupados;
            lblDashboardOcupacao.Content = ocupacao.ToString("0.00%");

            lblDashboardQuartosDisp.UpdateLayout();
            lblDashboardOcupacao.UpdateLayout();

            if (datagridDashboard.IsLoaded)
                CorrectDatagridHeaders();

            var now = DateTime.UtcNow;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            var lastDayofMonth = new DateTime(now.Year, now.Month, DaysInMonth);

            var valorMes = currentReservas
                .Where(r => r.DataEntrada >= startOfMonth && r.DataEntrada <= lastDayofMonth)
                .Sum(r => r.ValorDiaria * r.TempoEstadia.Days);

            var valorSemana = valorMes / 4.345m;
            var valorDia = valorSemana / 7m;

            labelDashboardFluxoDia.Content = $"Dia: R${valorDia:F2}";
            labelDashboardFluxoSemana.Content = $"Semana: R${valorSemana:F2}";
            labelDashboardFluxoMes.Content = $"Mês: R${valorMes:F2}";

            UpdateLayout();
        }

        public void SetDashboardDataGrid(DateTime date)
        {
            datagridDashboard.ItemsSource = currentDatalist.Where(d =>
                d.Horário.Day.Equals(date.Day) &&
                d.Horário.Month.Equals(date.Month) &&
                d.Horário.Year.Equals(date.Year));
            datagridDashboard.UpdateLayout();
            datagridDashboard.Items.Refresh();
        }

        private void CorrectDatagridHeaders(object sender = null, RoutedEventArgs e = null)
        {
            foreach (var prop in typeof(DashboardReservaDataRow).GetProperties())
            {
                var column = datagridDashboard.Columns.FirstOrDefault(c => c.Header.ToString().Equals(prop.Name, StringComparison.InvariantCultureIgnoreCase));
                var displayName = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;

                if (column != null)
                    column.Header = displayName.DisplayName;
            }
        }

        private void calendarDashboard_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDashboardDataGrid(calendarDashboard.SelectedDate.GetValueOrDefault().ToUniversalTime());
        }
    }
}
