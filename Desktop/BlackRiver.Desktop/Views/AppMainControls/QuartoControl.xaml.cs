using BlackRiver.Desktop.Extensions;
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
        private List<QuartoDataRow> quartoDataViewList = new();

        public QuartoControl()
        {
            InitializeComponent();
            UpdateControlData();
        }

        public async void UpdateControlData()
        {
            var quartoList = await BlackRiverAPI.GetQuartos();
            quartoDataViewList.Clear();

            foreach (var quarto in quartoList)
            {
                quartoDataViewList.Add(new QuartoDataRow
                {
                    Numero = quarto.Id,
                    Andar = quarto.NumeroAndar,
                    Status = ((QuartoStatus)quarto.StatusQuarto).ToString(),
                    Tipo = ((QuartoTypes)quarto.TipoQuarto).ToString(),
                    Vip = quarto.Vip
                });
            }

            datagridQuartos.ItemsSource = quartoDataViewList;
            datagridQuartos.UpdateLayout();
        }

        private void btnAddQuarto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new CriarQuartoWindow().SafeShowDialog();
            UpdateLayout();
        }

        private void btnEditarQuarto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new EditarQuartoWindow().SafeShowDialog();
            UpdateLayout();
        }

        private void btnQuartoRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateLayout();
        }

        [Serializable]
        public class QuartoDataRow
        {
            public int Numero { get; set; }
            public int Andar { get; set; }
            public string Status { get; set; }
            public string Tipo { get; set; }
            public bool Vip { get; set; }
        }
    }
}
