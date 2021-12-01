using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for HospedeControl.xaml
    /// </summary>
    public partial class HospedeControl : UserControl, IControlUpdate
    {
        private List<Hospede> hospedeList;
        private List<HospedeDataRow> funcionarioDataViewList = new();

        public HospedeControl()
        {
            InitializeComponent();
        }

        public async void UpdateControlData()
        {
            hospedeList = await BlackRiverAPI.GetHospedes();

            datagridHospede.UnselectAllCells();
            datagridHospede.UpdateLayout();

            datagridHospede.ItemsSource = null;
            funcionarioDataViewList.Clear();
            datagridHospede.ItemsSource = funcionarioDataViewList;

            foreach (var hospede in hospedeList)
            {
                var hospedeRow = new HospedeDataRow
                {
                    Nome = hospede.Nome,
                    Telefone = hospede.Telefone,
                    Email = hospede.Email,
                };

                funcionarioDataViewList.Add(hospedeRow);
            }
            datagridHospede.UpdateLayout();
            UpdateLayout();
            datagridHospede.Items.Refresh();
        }

        private async void btnNovoHospede_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                new CriarHospedeWindow().SafeShowDialog();

                await Application.Current.Dispatcher.Invoke(async delegate
                {
                    await Task.Delay(1000);
                    UpdateControlData();
                    UpdateLayout();
                });
            }
            catch (Exception)
            {
            }
        }

        private async void btnEditarHospede_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var row = datagridHospede.SelectedItems[0];
                var index = datagridHospede.Items.IndexOf(row);

                new EditarHospedeWindow(hospedeList[index]).SafeShowDialog();

                await Application.Current.Dispatcher.Invoke(async delegate
                {
                    await Task.Delay(1000);
                    UpdateControlData();
                    UpdateLayout();
                });
            }
            catch (Exception)
            {
                BlackRiverExtensions.ShowMessage("Falha ao editar, verifique se uma reserva está selecionada", "Erro");
            }
        }

        private void btnRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateControlData();
        }
    }
}
