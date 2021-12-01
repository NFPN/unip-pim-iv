using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ReservaControl.xaml
    /// </summary>
    public partial class ReservaControl : UserControl
    {
        private List<Reserva> reservaList;
        private List<ReservaDataRow> ReservaDataViewList = new();

        public ReservaControl()
        {
            InitializeComponent();
            UpdateControlData();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateControlData();
            UpdateLayout();
        }

        private void btnAddReserva_Click(object sender, RoutedEventArgs e)
        {
            new CriarReservaWindow().SafeShowDialog();
            UpdateControlData();
            UpdateLayout();
        }

        private void btnEditarReserva_Click(object sender, RoutedEventArgs e)
        {
            var row = datagridReserva.SelectedItems[0];
            var index = datagridReserva.Items.IndexOf(row);

            new EditarReservaWindow(reservaList[index]).SafeShowDialog();
            UpdateControlData();
            UpdateLayout();
        }

        public async void UpdateControlData()
        {
            reservaList = await BlackRiverAPI.GetReservas();

            datagridReserva.UnselectAllCells();
            datagridReserva.ItemsSource = null;
            datagridReserva.UpdateLayout();
            ReservaDataViewList.Clear();
            datagridReserva.ItemsSource = ReservaDataViewList;

            foreach (var reserva in reservaList)
            {
                var funcionarioRow = new ReservaDataRow
                {
                    DataEntrada = reserva.DataEntrada,
                    DataSaida = reserva.DataSaida,
                    Cancelado = reserva.DataCancelamento != null,
                };

                ReservaDataViewList.Add(funcionarioRow);
                datagridReserva.UpdateLayout();
            }
        }
    }
}
