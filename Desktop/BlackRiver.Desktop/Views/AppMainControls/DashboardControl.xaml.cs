using BlackRiver.Desktop.Extensions;
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
        private List<DashboardReservaDataRow> currentDatalist = new();

        public DashboardControl()
        {
            InitializeComponent();
        }

        public async void UpdateControlData()
        {
            currentReservas = await BlackRiverAPI.GetReservas();
            currentHospedes = await BlackRiverAPI.GetHospedes();

            foreach (var item in currentReservas)
            {
                currentDatalist.Add(new DashboardReservaDataRow
                {
                    Nome = currentHospedes.FirstOrDefault(h => h.Id == item.HospedeId).Nome,
                    Horário = item.DataEntrada,
                    NumeroQuarto = item.QuartoId,
                    Status = item.Status.Equals("Reservado", StringComparison.InvariantCultureIgnoreCase)
                });
            }

            var todosQuartos = await BlackRiverAPI.GetQuartos();
            var quartosOcupados = todosQuartos.Where(q => q.StatusQuarto is ((int)QuartoStatus.Ocupado) or ((int)QuartoStatus.Indisponível)).Count();
            var ocupacao = quartosOcupados == 0 ? 0 : (double)quartosOcupados / todosQuartos.Count;

            lblDashboardQuartosDisp.Content = todosQuartos.Count - quartosOcupados;
            lblDashboardOcupacao.Content = ocupacao.ToString("0.00%");

            lblDashboardQuartosDisp.UpdateLayout();
            lblDashboardOcupacao.UpdateLayout();

            SetDashboardDataGrid(DateTime.UtcNow);

            if (datagridDashboard.IsLoaded)
                CorrectDatagridHeaders();
        }

        public void SetDashboardDataGrid(DateTime date)
        {
            datagridDashboard.ItemsSource = currentDatalist.Where(d =>
                d.Horário.Day.Equals(date.Day) &&
                d.Horário.Month.Equals(date.Month) &&
                d.Horário.Year.Equals(date.Year));
            datagridDashboard.UpdateLayout();
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

        private void btnDashboardNovaReserva_Click(object sender, RoutedEventArgs e)
        {
            new CriarReservaWindow().SafeShowDialog();
        }

        private void btnDashboardEditReserva_Click(object sender, RoutedEventArgs e)
        {
            new EditarReservaWindow().SafeShowDialog();
        }
    }
}
