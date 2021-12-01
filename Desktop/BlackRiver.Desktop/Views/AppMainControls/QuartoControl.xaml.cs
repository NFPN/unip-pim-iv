using BlackRiver.Desktop.Extensions;
using BlackRiver.EntityModels;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace BlackRiver.Desktop.Views
{
    /// <summary>
    /// Interaction logic for QuartoControl.xaml
    /// </summary>
    public partial class QuartoControl : UserControl, IControlUpdate
    {
        private List<Quarto> quartoList;
        private List<QuartoDataRow> quartoDataViewList = new();

        public QuartoControl()
        {
            InitializeComponent();
        }

        public async void UpdateControlData()
        {
            quartoList = await BlackRiverAPI.GetQuartos();

            datagridQuartos.UnselectAllCells();
            datagridQuartos.ItemsSource = null;
            datagridQuartos.UpdateLayout();
            quartoDataViewList.Clear();
            datagridQuartos.ItemsSource = quartoDataViewList;

            foreach (var quarto in quartoList)
            {
                var quartoRow = new QuartoDataRow
                {
                    Numero = quarto.Id,
                    Andar = quarto.NumeroAndar,
                    Status = ((QuartoStatus)quarto.StatusQuarto).ToString(),
                    Tipo = ((QuartoTypes)quarto.TipoQuarto).ToString(),
                    Vip = quarto.Vip,
                    Diaria = quarto.ValorQuarto,
                };
                quartoDataViewList.Add(quartoRow);
            }
            datagridQuartos.UpdateLayout();
            datagridQuartos.Items.Refresh();
        }

        private void btnAddQuarto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new CriarQuartoWindow().SafeShowDialog();
            UpdateControlData();
            UpdateLayout();
        }

        private void btnEditarQuarto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var row = datagridQuartos.SelectedItems[0];
                var index = datagridQuartos.Items.IndexOf(row);

                new EditarQuartoWindow(quartoList[index]).SafeShowDialog();
                UpdateControlData();
                UpdateLayout();
            }
            catch (Exception)
            {
                BlackRiverExtensions.ShowMessage("Falha ao editar, verifique se um quarto está selecionado", "Erro");
            }
        }

        private void btnRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateControlData();
            UpdateLayout();
        }
    }
}
