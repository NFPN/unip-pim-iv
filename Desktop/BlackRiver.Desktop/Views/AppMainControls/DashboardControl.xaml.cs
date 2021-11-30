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
        private List<DashboardReservaDataItem> currentDatalist = new();

        public DashboardControl()
        {
            InitializeComponent();
            UpdateControlData();

            datagridDashboard.Loaded += CorrectColumHeaders;
        }

        public async void UpdateControlData()
        {
            //LoadMockData();

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

            var todosQuartos = await BlackRiverAPI.GetQuartos();
            var quartosDisponiveis = todosQuartos.Select(q => q.StatusQuarto == (int)QuartoStatus.Disponivel).Count();
            var ocupacao = (todosQuartos.Count / 100.0) * quartosDisponiveis;

            lblDashboardQuartosDisp.Content = todosQuartos.Select(q=>q.StatusQuarto);
            lblDashboardOcupacao.Content = ocupacao.ToString("0.00%");

            SetDashboardDataGrid(DateTime.UtcNow);
        }

        private void LoadMockData()
        {
            var rand = new Random();
            for (int i = 0; i < 30; i++)
            {
                var status = rand.Next(1000) > 500 ? "Não confirmado" : "Reservado";

                currentReservas.Add(
                    new Reserva
                    {
                        Id = rand.Next(1000),
                        ValorDiaria = (decimal)(rand.NextDouble() * 27 * rand.NextDouble() * 99),
                        Quarto = new Quarto
                        {
                            Id = rand.Next(1000),
                            NumeroAndar = rand.Next(10),
                            NumeroQuarto = rand.Next(1000),
                            StatusQuarto = rand.Next(2),
                        },
                        Hospedes = new List<Hospede>
                    {
                        new Hospede
                        {
                            Id = rand.Next(1000),
                            Nome = $"TEST{rand.Next(1000)}" ,
                            Email = $"TEST{rand.Next(1000)}@TEST.COM"
                        },
                    },
                        Status = status,
                        DataEntrada = DateTime.Today.AddHours(rand.Next(72)),
                    });
            }
        }

        public void SetDashboardDataGrid(DateTime date)
        {
            datagridDashboard.ItemsSource = currentDatalist.Where(d =>
                d.Horário.Day.Equals(date.Day) &&
                d.Horário.Month.Equals(date.Month) &&
                d.Horário.Year.Equals(date.Year));
            datagridDashboard.UpdateLayout();
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
