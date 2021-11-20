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
        public DashboardControl()
        {
            InitializeComponent();
            UpdateControlData();

            datagridDashboard.Loaded += CorrectColumHeaders;
        }

        public void UpdateControlData()
        {
            //datagridDashboard.ItemsSource = LoadCollectionData();
            MockDataGrid();
            //TODO: refresh data
        }

        public void MockDataGrid()
        {
            var mockReservas = new List<Reserva>();

            var rand = new Random();

            for (int i = 0; i < 10; i++)
            {
                var status = rand.Next(1000) > 500 ? "Não confirmado" : "Reservado";

                mockReservas.Add(
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
                        DataEntrada = DateTime.Today.AddHours(rand.Next(1000)),
                    });

                status = rand.Next() > 300 ? "Não confirmado" : "Reservado";

                mockReservas.Add(
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
                        DataEntrada = DateTime.Today.AddHours(rand.Next(1000)),
                    });
            }

            var dashboardDataList = new List<DashboardReservaDataItem>();

            foreach (var item in mockReservas)
            {
                dashboardDataList.Add(new DashboardReservaDataItem
                {
                    Nome = item.Hospedes.First().Nome,
                    Horário = item.DataEntrada,
                    NumeroQuarto = item.Quarto.NumeroQuarto,
                    Status = item.Status.Equals("Reservado", StringComparison.InvariantCultureIgnoreCase)
                });
            }

            datagridDashboard.ItemsSource = dashboardDataList;
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
            //Update dashboard grid to show Reservas only on that day
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
