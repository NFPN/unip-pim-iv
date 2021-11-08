using BlackRiver.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        }

        public void UpdateControlData()
        {
            //datagridDashboard.ItemsSource = LoadCollectionData();
            MockDataGrid();
            //TODO: refresh data
        }

        public void MockDataGrid()
        {
            var mockReservas = new List<Reserva>
            {
                new Reserva
                {
                    Id  = 0,
                    ValorDiaria = 100.0m,
                    Quarto = new Quarto
                    {
                        Id = 0,
                        NumeroAndar = 1,
                        NumeroQuarto = 45,
                        StatusQuarto = 1,
                    },
                    Hospedes = new List<Hospede>
                    {
                        new Hospede
                        {
                            Id = 345,
                            Nome = "TEST01",
                            Email = "TEST@TEST.COM"
                        },
                    },
                    Status = "Reservado",
                    DataEntrada = DateTime.Today.AddHours(9),
                },

                new Reserva
                {
                    Id  = 1,
                    ValorDiaria = 123.0m,
                    Quarto = new Quarto
                    {
                        Id = 1,
                        NumeroAndar = 1,
                        NumeroQuarto = 44,
                        StatusQuarto = 1,
                    },
                    Hospedes = new List<Hospede>
                    {
                        new Hospede
                        {
                            Id = 34,
                            Nome = "TEST02",
                            Email = "TEST2@TEST.COM"
                        },
                    },
                    Status = "Não confirmado",
                    DataEntrada = DateTime.Today.AddHours(6),
                },
            };

            var dashboardDataList = new List<DashboardReserva>();

            foreach (var item in mockReservas)
            {
                dashboardDataList.Add(new DashboardReserva
                {
                    Nome = item.Hospedes.First().Nome,
                    Horário = item.DataEntrada,
                    NumeroQuarto = item.Quarto.NumeroQuarto,
                    Status = item.Status.Equals("Reservado", StringComparison.InvariantCultureIgnoreCase)
                });
            }

            datagridDashboard.ItemsSource = dashboardDataList;
        }

        
    }

    [Serializable]
    public class DashboardReserva
    {
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [DisplayName("Horário")]
        public DateTime Horário { get; set; }

        [DisplayName("Numero Quarto")]
        public int NumeroQuarto { get; set; }

        [DisplayName("Reservado")]
        public bool Status { get; set; }
    }
}
