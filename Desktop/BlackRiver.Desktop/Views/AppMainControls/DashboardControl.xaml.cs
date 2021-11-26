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
        private List<DashboardReservaDataItem> currentDatalist = new();

        public DashboardControl()
        {
            InitializeComponent();
            UpdateControlData();

            datagridDashboard.Loaded += CorrectColumHeaders;
        }

        public void UpdateControlData()
        {
            foreach (var item in currentReservas)
            {
                currentDatalist.Add(new DashboardReservaDataItem
                {
                    Nome = item.Hospedes.First().Nome,
                    Horário = item.DataEntrada,
                    NumeroQuarto = item.Quarto.NumeroQuarto,
                    Status = item.Status.Equals("Reservado", StringComparison.InvariantCultureIgnoreCase)
                });
            }

            SetDashboardDataGrid(DateTime.UtcNow);
        }

        public void SetDashboardDataGrid(DateTime date)
        {
            datagridDashboard.ItemsSource = currentDatalist.Where(d =>
                d.Horário.Day.Equals(date.Day) &&
                d.Horário.Month.Equals(date.Month) &&
                d.Horário.Year.Equals(date.Year));
        }

        private void CorrectColumHeaders(object sender = null, RoutedEventArgs e = null)
        {
            foreach (var prop in typeof(DashboardReservaDataItem).GetProperties())
            {
                var column = datagridDashboard.Columns.FirstOrDefault(c => c.Header.ToString().Equals(prop.Name, StringComparison.InvariantCultureIgnoreCase));
                var displayName = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute;

                if (column != null)
                    column.Header = displayName.DisplayName;
            }
        }

        private void calendarDashboard_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            SetDashboardDataGrid(e.AddedDate.GetValueOrDefault().ToUniversalTime());
        }
    }

    [Serializable]
    public class DashboardReservaDataItem
    {
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Horário de chegada")]
        public DateTime Horário { get; set; }

        [DisplayName("Numero do Quarto")]
        public int NumeroQuarto { get; set; }

        [DisplayName("Reservado")]
        public bool Status { get; set; }
    }
}
