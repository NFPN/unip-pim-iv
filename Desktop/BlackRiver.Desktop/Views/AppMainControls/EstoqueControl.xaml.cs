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
    /// Interaction logic for EstoqueControl.xaml
    /// </summary>
    public partial class EstoqueControl : UserControl, IControlUpdate
    {
        private List<Produto> estoqueList;
        private List<ProdutoDataRow> estoqueDataViewList = new();

        public EstoqueControl()
        {
            InitializeComponent();
        }

        public async void UpdateControlData()
        {
            estoqueList = await BlackRiverAPI.GetProdutos();

            datagridEstoque.UnselectAllCells();
            datagridEstoque.ItemsSource = null;
            datagridEstoque.UpdateLayout();
            estoqueDataViewList.Clear();
            datagridEstoque.ItemsSource = estoqueDataViewList;

            foreach (var produto in estoqueList)
            {
                var produtoRow = new ProdutoDataRow
                {
                    Nome = produto.Nome,
                    Tipo = produto.Tipo,
                    Valor = produto.Valor,
                    Quantidade = produto.QuantidadeDisponivel,
                };
                estoqueDataViewList.Add(produtoRow);
            }
            datagridEstoque.UpdateLayout();
        }

        private async void btnAddProduto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new CriarProdutoWindow().SafeShowDialog();
            await Application.Current.Dispatcher.Invoke(async delegate
            {
                await Task.Delay(1000);
                UpdateControlData();
                UpdateLayout();
            });
        }

        private async void btnEditarProduto_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var row = datagridEstoque.SelectedItems[0];
            var index = datagridEstoque.Items.IndexOf(row);

            new EditarProdutoWindow(estoqueList[index]).SafeShowDialog();

            await Application.Current.Dispatcher.Invoke(async delegate
            {
                await Task.Delay(1000);
                UpdateControlData();
                UpdateLayout();
            });
        }

        private async void btnRefresh_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.Invoke(async delegate
            {
                await Task.Delay(500);
                UpdateControlData();
                UpdateLayout();
            });
        }
    }

    [Serializable]
    public class ProdutoDataRow
    {
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
    }
}
